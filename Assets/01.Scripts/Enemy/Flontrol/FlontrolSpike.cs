using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontrolSpike : PoolableMono
{
    private DamageCaster _damageCaster;
    private Animator _anim;
    private void Awake()
    {
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        _anim = transform.Find("Visual").GetComponent<Animator>();
    }
    public override void Init()
    {

    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
}
