using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthShare : MonoBehaviour,IDamageable
{
    private Entity _owner;
    private Health _mainHealth;
    [SerializeField] private AilmentStat _ailmentStat; //���� �� ����� ���� ����
    public int maxHealth;
    private int _currentHealth;

    public bool isDead;

    public Action<int> OnHit;
    public UnityEvent<Ailment> OnAilmentChanged;

    private void Awake()
    {
        _ailmentStat = new AilmentStat();
        _ailmentStat.EndOFAilmentEvent += HandleEndOfAilment;
        _ailmentStat.AilmentDamageEvent += HandleAilementDamage;

        OnHit += OnHitHandle;
    }
    private void OnDestroy()
    {
        _ailmentStat.EndOFAilmentEvent -= HandleEndOfAilment;
        _ailmentStat.AilmentDamageEvent -= HandleAilementDamage;
        
        OnHit -= OnHitHandle;
    }

    private void OnHitHandle(int daamage)
    {
        _mainHealth.HealthShareDamage(daamage);
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
        if (isDead) return;
        Debug.Log($"{ailment.ToString()} dot damaged : {damage}");
        OnHit?.Invoke(damage);
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }

    protected void Update()
    {
        _ailmentStat.UpdateAilment(); //���� ������Ʈ
    }

    public void Init(Entity owner, Health main, float value)
    {
        _owner = owner;
        _mainHealth = main;
        maxHealth = main.maxHealth;
        _currentHealth = Mathf.FloorToInt(main.maxHealth * value);
    }
    public void ApplyHealth(int health, bool isResurrection = false)
    {
        if (isDead && !isResurrection) return;
        _currentHealth += health;
        if (isResurrection) isDead = false; 
    }

    public void ApplyDamage(int damage, Vector2 attackDirection, Vector2 knockbackPower, Entity dealer)
    {
        if(isDead)
        {
            return;
        }

        bool isLastHitCritical;
        if (_owner.CharStat.CanEvasion())
        {
            Debug.Log($"{_owner.gameObject.name} is evasion attack!");
            return;
        }
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

        //���⼭ ������ ����ֱ�
        DamageTextManager.Instance.PopupDamageText(_owner.transform.position, damage, isLastHitCritical ? DamageCategory.Critical : DamageCategory.Noraml);

        //���������� üũ
        CheckAilmentByDamage(damage);
        OnHit?.Invoke(_currentHealth - damage < 0 ? _currentHealth : damage);

        if (_currentHealth <= 0) isDead = true;
    }

    private void CheckAilmentByDamage(int damage)
    {
        //��ũ������ �߰� �κ�.
        if (_ailmentStat.HasAilment(Ailment.Shocked)) //��ũ �����̻��� �ִٸ� �������� 10% �ߵ� 
        {
            int shockDamage = Mathf.Min(3, Mathf.RoundToInt(damage * 0.1f));
            _currentHealth = Mathf.Clamp(_currentHealth - shockDamage, 0, maxHealth);

            OnHit?.Invoke(_currentHealth - damage < 0 ? _currentHealth : damage);
            //������� ������ �ؽ�Ʈ �߰�
            //DamageTextManager.Instance.PopupDamageText(_owner.transform.position, shockDamage, DamageCategory.Debuff);
            //Debug.Log($"{gameObject.name} : shocked damage added = {shockDamage}");
        }
    }

    public void SetAilment(Ailment ailment, float duration, int damage)
    {
        _ailmentStat.ApplyAilments(ailment, duration, damage);
        OnAilmentChanged?.Invoke(_ailmentStat.currentAilment);
    }
}
