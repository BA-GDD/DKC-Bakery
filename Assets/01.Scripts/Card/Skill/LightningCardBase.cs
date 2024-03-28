using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LightningCardBase : CardBase
{
    [SerializeField] private ParticleSystem _shockedEffect;

    protected void ExtraAttack()
    {
        foreach (var e in battleController.onFieldMonsterList)
        {
            Debug.Log("ddd");
            try
            {
                e?.HealthCompo.AilmentStat.UsedToAilment(AilmentEnum.Shocked);
                GameObject shockedEffects = Instantiate(_shockedEffect.gameObject, Player.target.transform.position, Quaternion.identity);
                Destroy(shockedEffects, 1.0f);
            }
            catch (Exception ex)
            {
                Debug.Log(e);
            }
        }
    }
}
