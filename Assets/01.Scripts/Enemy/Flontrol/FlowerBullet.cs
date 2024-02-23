using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : PoolableMono
{
    [SerializeField] private Vector2 knockbackPower;
    private Flontrol _enemy;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public override void Init()
    {
    }
    public void SetOwner(Flontrol entity, Vector2 dir)
    {
        _enemy = entity;
        _rigidbody.velocity = dir.normalized * 10;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            Vector2 knockbackVector = new Vector2(collision.transform.position.x - transform.position.x,1);
            damageable.ApplyDamage(_enemy.CharStat.GetDamage(), knockbackVector, knockbackPower, _enemy);
            PoolManager.Instance.Push(this);
        }
    }

}
