using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Flags]
public enum Ailment : int
{
    None = 0,
    Ignited = 1, // ��Ʈ������ �ִ� ȿ�� 3�ʿ� ���� 0.3�ʴ� 3�� 
    Chilled = 2, // 4�ʰ� �Ƹ� -20 ����
    Shocked = 4  // �ǰݽø��� ��ũ ������ �߰�.(�޴� �������� 10%, �ּ� 3������)
}
public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;

    [SerializeField] private int _currentHealth;

    public Action<Color, int> OnDamageText; //������ �ؽ�Ʈ�� ����� �Ҷ�.
    public Action<float, float> OnDamageEvent;

    public UnityEvent OnDeathEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent<Ailment> OnAilmentChanged;

    private Entity _owner;
    public bool isDead = false;
    private bool _isInvincible = false; //��������
    [SerializeField] private AilmentStat _ailmentStat; //���� �� ����� ���� ����
    public AilmentStat AilmentStat => _ailmentStat;

    public bool isLastHitCritical = false; //������ ������ ũ��Ƽ�÷� �����߳�?

    protected void Awake()
    {
        _ailmentStat = new AilmentStat();
        _ailmentStat.EndOFAilmentEvent += HandleEndOfAilment;
        _ailmentStat.AilmentDamageEvent += HandleAilementDamage;

        isDead = false;
    }
    private void OnDestroy()
    {
        _ailmentStat.EndOFAilmentEvent -= HandleEndOfAilment;
        _ailmentStat.AilmentDamageEvent -= HandleAilementDamage;
    }

    private void HandleEndOfAilment(Ailment ailment)
    {
        Debug.Log($"{gameObject.name} : cure from {ailment.ToString()}");
        //���⼭ ������ ���ŵ��� �ϵ��� �Ͼ�� �Ѵ�.
        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);

    }

    private void HandleAilementDamage(Ailment ailment, int damage)
    {
        //������ ���� ���ڰ� �ߵ��� �ؾ��Ѵ�.
        Debug.Log($"{ailment.ToString()} dot damaged : {damage}");
        OnHitEvent?.Invoke();
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }

    protected void Update()
    {
        _ailmentStat.UpdateAilment(); //���� ������Ʈ
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
        _currentHealth = maxHealth = _owner.CharStat.GetMaxHealthValue();
    }

    public float GetNormalizedHealth()
    {
        if (maxHealth <= 0) return 0;
        Debug.Log($"{_currentHealth}, {maxHealth}");
        return Mathf.Clamp((float)_currentHealth / maxHealth, 0, 1f);
    }

    public void ApplyHeal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        //ü�������� ���� UI�ʿ�.
        Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
    }

    public void ApplyTrueDamage(int damage, Entity dealer)
    {
        if (isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }
    public void ApplyDamage(int damage, Entity dealer)
    {
        if (isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.

        //�Ϻ� ȸ�� ���.
        if (_owner.CharStat.CanEvasion())
        {
            Debug.Log($"{_owner.gameObject.name} is evasion attack!");
            return;
        }
        //ũ��Ƽ��Ȯ���� ���� ũ��Ƽ������ Ȯ���ϰ� ������ ����
        if (dealer.CharStat.IsCritical(ref damage))
        {
            Debug.Log($"Critical! : {damage}"); //������ �����Ǿ���.
            isLastHitCritical = true;
        }
        else
        {
            isLastHitCritical = false;
        }

        //�ƸӰ��� ���� ������ ����. ����ÿ��� �Ƹ� ����.
        damage = _owner.CharStat.ArmoredDamage(damage, _ailmentStat.HasAilment(Ailment.Chilled));

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        OnDamageEvent?.Invoke(_currentHealth, maxHealth);


        //���⼭ ������ ����ֱ�
        DamageTextManager.Instance.PopupDamageText(_owner.transform.position, damage, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);
        //DamageTextManager.Instance.PopupReactionText(_owner.transform.position, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);

        //���������� üũ
        CheckAilmentByDamage(damage);
        AfterHitFeedbacks();
    }

    public void ApplyMagicDamage(int damage, Vector2 attackDirection, Vector2 knockbackPower, Entity dealer)
    {
        int magicDamage = _owner.CharStat.GetMagicDamageAfterRegist(damage);
        _currentHealth = Mathf.Clamp(_currentHealth - magicDamage, 0, maxHealth);
        Debug.Log($"apply magic damage to {_owner.gameObject.name}! : {damage}");

        knockbackPower.x *= attackDirection.x; //y���� ��������.

        //������ ����
        //DamageTextManager.Instance.PopupDamageText(_owner.transform.position, magicDamage, DamageCategory.Noraml);
        AfterHitFeedbacks();
    }

    private void AfterHitFeedbacks()
    {

        if (_currentHealth == 0)
        {
            isDead = true;
            OnDeathEvent?.Invoke();
            return;
        }
        OnHitEvent?.Invoke();
    }

    //�����̻� �ɱ�.
    public void SetAilment(Ailment ailment, float duration, int damage)
    {
        _ailmentStat.ApplyAilments(ailment, duration, damage);
        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);
    }

    //�������� �޾��� �� ���� üũ�ϴ� �Լ�(��ũ ������ ���� Ÿ�ݴ� �������� ����.
    private void CheckAilmentByDamage(int damage)
    {
        //��ũ������ �߰� �κ�.
        if (_ailmentStat.HasAilment(Ailment.Shocked)) //��ũ �����̻��� �ִٸ� �������� 10% �ߵ� 
        {
            int shockDamage = Mathf.Min(3, Mathf.RoundToInt(damage * 0.1f));
            _currentHealth = Mathf.Clamp(_currentHealth - shockDamage, 0, maxHealth);

            //������� ������ �ؽ�Ʈ �߰�
            //DamageTextManager.Instance.PopupDamageText(_owner.transform.position, shockDamage, DamageCategory.Debuff);
            //Debug.Log($"{gameObject.name} : shocked damage added = {shockDamage}");
        }
    }


    public void MakeInvincible(bool value)
    {
        _isInvincible = value;
    }


}