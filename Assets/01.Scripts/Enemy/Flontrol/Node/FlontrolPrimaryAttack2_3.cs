using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BTVisual
{
    public class FlontrolPrimaryAttack2_3 : FlontrolPattrenNode
    {
        private int _invokeCnt;
        private bool _useLeftArm;
        protected override void OnStart()
        {
            base.OnStart();
            enemy.animationEvent += Attack;
            _useLeftArm = enemy.transform.position.x < GameManager.Instance.PlayerTrm.position.x;
        }

        protected override void OnStop()
        {
            base.OnStop();

            enemy.animationEvent -= Attack;
            _invokeCnt = 0;
            enemy.lastTimeAttacked = Time.time;
        }
        protected override State OnUpdate()
        {
            if(_invokeCnt == 1)
            {
                if (_useLeftArm)
                    foreach (var d in enemy.leftArmDamageCast)
                        d.CastDamage();
                else
                    foreach (var d in enemy.rightArmDamageCast)
                        d.CastDamage();
            }
            if (enemy.endAnimationTrigger)
            {
                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        private void Attack()
        {
            _invokeCnt++;
            if(_invokeCnt == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    FlontrolStone stone = PoolManager.Instance.Pop(PoolingType.FlontrolStone) as FlontrolStone;
                    stone.transform.position = _useLeftArm ? enemy.leftHandTrm.position : enemy.rightHandTrm.position;
                    float angle = Random.Range(10f, 90f) * Mathf.Deg2Rad;
                    Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 10;
                    if (_useLeftArm) dir.x *= -1;
                    stone.SetOwner(enemy, dir);
                }
            }
        }
    }
}
