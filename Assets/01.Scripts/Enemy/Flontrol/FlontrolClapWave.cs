using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlontrolClapWave : MonoBehaviour
{
    private Flontrol _enemy;
    private Player _player;
    private float _timer;
    private bool _isActive;

    [SerializeField] private float _maxRadius;
    [SerializeField] private float _expansionTime;
    [SerializeField] private float _judgmentDis;


    [SerializeField] private Vector2 knockbackPower;

    private void Start()
    {
        _enemy = transform.parent.GetComponent<Flontrol>();
        _player = GameManager.Instance.Player;
    }
    public void Wave()
    {
        _timer = 0;
        _isActive = true;
    }
    private void Update()
    {
        if (_isActive)
        {
            if (_timer < _expansionTime)
            {
                _timer += Time.deltaTime;
                float _radius = Mathf.Lerp(0, _maxRadius, _timer / _expansionTime);
                transform.localScale = new Vector3(_radius*2, _radius*2);

                float dis = _radius - Vector2.Distance(transform.position, _player.transform.position);

                Vector2 attackDirection = new Vector2(_player.transform.position.x - transform.position.x, 1);

                if (dis > 0 && dis < _judgmentDis)
                {
                    _player.GetComponent<IDamageable>().ApplyDamage(_enemy.CharStat.GetDamage(), attackDirection, knockbackPower, _enemy);
                }
            }
            else
            {
                _isActive = false;
                transform.localScale = new Vector3(0, 0);
            }
        }
    }
}
