using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class FlontrolPrimaryAttack1_4 : FlontrolPattrenNode
    {
        private int _invokeCnt;
        protected override void OnStart()
        {
            base.OnStart();

            enemy.animationEvent += Attack;
        }

        protected override void OnStop()
        {
            base.OnStop();
            _invokeCnt = 0;
            enemy.animationEvent -= Attack;
            enemy.lastTimeAttacked = Time.time;
        }

        protected override State OnUpdate()
        {
            if (enemy.endAnimationTrigger)
            {

                return State.SUCCESS;
            }
            return State.RUNNING;
        }
        private void Attack()
        {
            _invokeCnt++;
            if (_invokeCnt < 3)
            {
                Transform player = GameManager.Instance.PlayerTrm;
                float angleDiff = 100 / 6;

                Vector2 tempVec = player.transform.position - enemy.flowerShotTransfom.position;
                float mainAsix = Mathf.Atan2(tempVec.y, tempVec.x) - 50 * Mathf.Deg2Rad;
                for (int i = 0; i < 6; i++)
                {

                    FlowerBullet bullet = PoolManager.Instance.Pop(PoolingType.FlontrolBullet) as FlowerBullet;
                    bullet.transform.position = enemy.flowerShotTransfom.position;
                    float angle = Mathf.Deg2Rad * (angleDiff * (i + 1)) + mainAsix;
                    Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    bullet.SetOwner(enemy, dir);
                }
            }
            else
            {
                for (int i = 0; i < 24; i++)
                {

                    float angleDiff = 360 / 24;
                    FlowerBullet bullet = PoolManager.Instance.Pop(PoolingType.FlontrolBullet) as FlowerBullet;
                    bullet.transform.position = enemy.flowerShotTransfom.position;
                    float angle = Mathf.Deg2Rad * (angleDiff * i);
                    Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    bullet.SetOwner(enemy, dir);
                }
            }
        }
    }
}