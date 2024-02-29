using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontrolSpike : PoolableMono
{
    private Flontrol _owner;
    private DamageCaster _damageCaster;
    private Animator _anim;
    private void Start()
    {
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
        _damageCaster.SetOwner(_owner);
    }
    public override void Init()
    {

    }
    public void Bind(Flontrol owner)
    {
        _owner = owner;
    }
    public void Attack(bool afterGotoPool = false)
    {
        _anim.SetTrigger("Attack");
        _damageCaster.CastDamage();
        StartCoroutine(AttackAfter(afterGotoPool));   
    }
    private IEnumerator AttackAfter(bool afterGotoPool)
    {
        yield return new WaitForSeconds(1f);
        if(afterGotoPool)
        {
            PoolManager.Instance.Push(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
