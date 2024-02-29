using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : PoolableMono
{
    private DamageCaster _damageCaster;

    private Flontrol _enemy;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _damageCaster = GetComponent<DamageCaster>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }
    public override void Init()
    {
    }
    public void SetOwner(Flontrol entity, Vector2 dir)
    {
        _enemy = entity;
        _damageCaster.SetOwner(entity);
        _rigidbody.velocity = dir.normalized * 10;
    }
    public void Update()
    {
        if(_damageCaster.CastDamage())
        {
            PoolManager.Instance.Push(this);
            return;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Stage"))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
