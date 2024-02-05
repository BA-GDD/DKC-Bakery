using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmFireBall : PoolableMono
{
    [SerializeField] private Vector2 _knockbackPower;
    private Rigidbody2D _rigid;
    private Warm _owner;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();    
    }
    public void Fire(int facingDir,Warm owner)
    {
        if(facingDir < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
        _owner = owner;
        _rigid.velocity = new Vector2(facingDir, 0) * _owner.fireBallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            DamageToTarget(player);
            PoolManager.Instance.Push(this);
        }
    }
    private void DamageToTarget(Player target)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        int damage = Mathf.RoundToInt(_owner.CharStat.GetDamage()); //배율에 따라 증뎀.
        target.HealthCompo.ApplyDamage(damage, direction, _knockbackPower, _owner);
    }
    public override void Init()
    {
    }
}
