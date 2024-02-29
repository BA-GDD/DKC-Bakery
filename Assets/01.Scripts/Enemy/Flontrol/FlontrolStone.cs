using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontrolStone : PoolableMono
{
    [SerializeField] private Vector2 _knockbackPower;
    [SerializeField] private LayerMask _whatIsGround;

    private Rigidbody2D _rigid2d;
    private DamageCaster _damageCaster;

    private bool _isStart;

    public override void Init()
    {

    }
    private void Awake()
    {
        _rigid2d = GetComponent<Rigidbody2D>();
        _damageCaster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
    }
    public void SetOwner(Flontrol enemy, Vector2 dir)
    {
        _isStart = true;
        _damageCaster.SetOwner(enemy);
        _rigid2d.AddForce(dir, ForceMode2D.Impulse);
    }

    private void Update()
    {
        _damageCaster.CastDamage();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isStart) return;
        Vector2 dir = collision.ClosestPoint(transform.position);
        if(Physics2D.Raycast(transform.position,dir.normalized,dir.magnitude,_whatIsGround))
        {
            PoolManager.Instance.Push(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isStart = false;
    }
}