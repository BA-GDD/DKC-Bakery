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
    [SerializeField] private bool _isDead = false;
    public bool IsDead
    {
        get => _isDead;
        set
        {
            _isDead = value;
            if (_isDead)
            {
                if (_owner is Enemy)
                    CardReader.SkillCardManagement.useCardEndEvnet.AddListener(_owner.DeadSeq);
                else
                    OnDeathEvent?.Invoke();
            }
        }
    }
    private bool _isInvincible = false; //��������
    [SerializeField] private AilmentStat _ailmentStat; //���� �� ����� ���� ����
    public AilmentStat AilmentStat => _ailmentStat;

    public bool isLastHitCritical = false; //������ ������ ũ��Ƽ�÷� �����߳�?

    public bool IsFreeze;

    protected void Awake()
    {
        _ailmentStat = new AilmentStat(this);


    }
    private void OnEnable()
    {
        TurnCounter.RoundEndEvent += _ailmentStat.UpdateAilment;
        _ailmentStat.EndOFAilmentEvent += HandleEndOfAilment;

        _isDead = false;
    }
    private void OnDisable()
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
        Debug.Log($"{_currentHealth}/{maxHealth}");
    }

    public float GetNormalizedHealth()
    {
        if (maxHealth <= 0) return 0;
        return Mathf.Clamp((float)_currentHealth / maxHealth, 0, 1f);
    }

    public void ApplyHeal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
        _owner.OnHealthBarChanged?.Invoke(GetNormalizedHealth());
    }

    public void ApplyTrueDamage(int damage)
    {
        if (_isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }
    public void ApplyDamage(int damage, Entity dealer, Action action = null)
    {
        _owner.BuffStatCompo.OnHitDamageEvent?.Invoke(dealer, ref damage);

        if (dealer.CharStat.IsCritical(ref damage))
        {
            Debug.Log($"Critical! : {damage}"); //������ �����Ǿ���.
            isLastHitCritical = true;
        }
        else
        {
            isLastHitCritical = false;
        }

        DamageTextManager.Instance.PopupDamageText(_owner.transform.position, damage, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);
        //�ƸӰ��� ���� ������ ����. ����ÿ��� �Ƹ� ����.
        damage = _owner.CharStat.ArmoredDamage(damage, IsFreeze);
        if (_isDead || _isInvincible) return; //����ϰų� �������¸� ���̻� ������ ����.

        //�Ϻ� ȸ�� ���.
        if (_owner.CharStat.CanEvasion())
        {
            Debug.Log($"{_owner.gameObject.name} is evasion attack!");
            return;
        }
        //ũ��Ƽ��Ȯ���� ���� ũ��Ƽ������ Ȯ���ϰ� ������ ����
        

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
        OnDamageEvent?.Invoke(_currentHealth, maxHealth);


        //���⼭ ������ ����ֱ�
        //DamageTextManager.Instance.PopupReactionText(_owner.transform.position, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);


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
        OnHitEvent?.Invoke();
        if (_currentHealth == 0)
        {
            IsDead = true;
            return;
        }
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

    //�����̻� �ɱ�.
    public void SetAilment(AilmentEnum ailment, int duration)
    {
        _ailmentStat.ApplyAilments(ailment, duration);

        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);
    }

    public void AilmentByDamage(AilmentEnum ailment, int damage)
    {
        //��ũ������ �߰� �κ�.
        //������� ������ �ؽ�Ʈ �߰�
        DamageTextManager.Instance.PopupDamageText(_owner.transform.position, damage, DamageCategory.Debuff);
        OnDamageEvent?.Invoke(_currentHealth, maxHealth);
        //Debug.Log($"{gameObject.name} : shocked damage added = {shockDamage}");
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