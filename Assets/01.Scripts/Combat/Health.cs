using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Flags]
public enum AilmentEnum : int
{
    None = 0,
    Chilled = 1,
    Shocked = 2
}
public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;

    [SerializeField] private int _currentHealth;

    public Action<Color, int> OnDamageText; //������ �ؽ�Ʈ�� ����� �Ҷ�.
    public Action<float, float> OnDamageEvent;

    public Action OnBeforeHit;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent<AilmentEnum> OnAilmentChanged;

    private Entity _owner;
    private bool _isDead = false;
    public bool IsDead
    {
        get => _isDead;
        set
        {
            OnDeathEvent?.Invoke();
            _isDead = value;
        }
    }
    private bool _isInvincible = false; //��������
    [SerializeField] private AilmentStat _ailmentStat; //���� �� ����� ���� ����
    public AilmentStat AilmentStat => _ailmentStat;

    public bool isLastHitCritical = false; //������ ������ ũ��Ƽ�÷� �����߳�?

    protected void Awake()
    {
        _ailmentStat = new AilmentStat(this);
        _ailmentStat.EndOFAilmentEvent += HandleEndOfAilment;
        TurnCounter.RoundEndEvent += _ailmentStat.UpdateAilment;

        _isDead = false;
    }
    private void OnDestroy()
    {
        _ailmentStat.EndOFAilmentEvent -= HandleEndOfAilment;
        TurnCounter.RoundEndEvent -= _ailmentStat.UpdateAilment;
    }

    private void HandleEndOfAilment(AilmentEnum ailment)
    {
        Debug.Log($"{gameObject.name} : cure from {ailment.ToString()}");
        //���⼭ ������ ���ŵ��� �ϵ��� �Ͼ�� �Ѵ�.
        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);

    }

    public void AilementDamage(AilmentEnum ailment, int damage)
    {
        //������ ���� ���ڰ� �ߵ��� �ؾ��Ѵ�.
        Debug.Log($"{ailment.ToString()} dot damaged : {damage}");
        OnHitEvent?.Invoke();
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        AfterHitFeedbacks();
    }

    protected void UpdateAilment()
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
        if (_isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }
    public void ApplyDamage(int damage, Entity dealer, Action action = null)
    {
        if (_isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.

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
        damage = _owner.CharStat.ArmoredDamage(damage, _ailmentStat.HasAilment(AilmentEnum.Chilled));

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        OnDamageEvent?.Invoke(_currentHealth, maxHealth);


        //���⼭ ������ ����ֱ�
        DamageTextManager.Instance.PopupDamageText(_owner.transform.position, damage, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);
        //DamageTextManager.Instance.PopupReactionText(_owner.transform.position, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);

        //���������� üũ
        AfterHitFeedbacks();

        action?.Invoke();
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
            IsDead = true;
            Debug.Log(1);
            return;
        }
        OnHitEvent?.Invoke();
    }

    [ContextMenu("Chilled")]
    private void Test1()
    {
        SetAilment(AilmentEnum.Chilled, 2);
    }
    [ContextMenu("Shocked")]
    private void Test2()
    {
        SetAilment(AilmentEnum.Shocked, 2);
    }
    [ContextMenu("asdf")]
    private void Test3()
    {
        print("asdf");
        TurnCounter.ChangeRound();
    }
    private void DeadProcess()
    {

    }

    //�����̻� �ɱ�.
    public void SetAilment(AilmentEnum ailment, int duration)
    {
        _ailmentStat.ApplyAilments(ailment, duration);
        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);
    }

    //�������� �޾��� �� ���� üũ�ϴ� �Լ�(��ũ ������ ���� Ÿ�ݴ� �������� ����.
    private void CheckAilmentByDamage(AilmentEnum ailment)
    {
        //��ũ������ �߰� �κ�.
        if (_ailmentStat.HasAilment(ailment)) //��ũ �����̻��� �ִٸ� �������� 10% �ߵ� 
        {
            int shockDamage = 0;
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
    [ContextMenu("TestHitFeedback")]
    private void TestDead()
    {
        AfterHitFeedbacks();
    }
}