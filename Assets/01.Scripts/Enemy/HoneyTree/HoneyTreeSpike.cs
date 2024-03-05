using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyTreeSpike : PoolableMono
{
    private readonly int _attackAnimHash = Animator.StringToHash("Attack");
    private DamageCaster _damageCaster;
    private Animator _animator;

    public override void Init()
    {
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
    }
    public void SetUp(HoneyTree owner)
    {
        _damageCaster.SetOwner(owner);
    }
    public void Attack()
    {
        _animator.SetTrigger(_attackAnimHash);
        _damageCaster.CastDamage();
        StartCoroutine(GotoPool());
    }
    private IEnumerator GotoPool()
    {
        yield return new WaitForSeconds(2f);
        PoolManager.Instance.Push(this);
    }
}
