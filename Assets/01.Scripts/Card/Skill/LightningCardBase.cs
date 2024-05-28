using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightningCardBase : CardBase
{
    [SerializeField] private ParticleSystem _shockedEffect;

    protected void ExtraAttack(LightningCardBase me)
    {
        foreach (var e in battleController.onFieldMonsterList)
        {
            try
            {
                Debug.Log("번개 체인");
                e?.HealthCompo.AilmentStat.UsedToAilment(AilmentEnum.Shocked);
                if(e != me)
                {
                    // 파티클 인포로 풀 하는 형식으로
                    // me에서 e로 가는 방향을 구하고 거기를 바라보게 한다
                }
                //GameObject shockedEffects = Instantiate(_shockedEffect.gameObject, Player.target.transform.position, Quaternion.identity);
                //Destroy(shockedEffects, 1.0f);
            }
            catch (Exception ex)
            {
                Debug.Log(e);
            }
        }
    }

    protected void ApplyShockedAilment(Entity enemy)
    {
        enemy.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
    }

    protected void RandomApplyShockedAilment(Entity enemy, float percentage)
    {
        if (UnityEngine.Random.value * 100 >= percentage)
            enemy.HealthCompo.AilmentStat.ApplyAilments(AilmentEnum.Shocked);
    }
}
