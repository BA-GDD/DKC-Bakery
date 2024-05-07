using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleDebuff : SpecialBuff, IOnEndSkill
{
    private int duration;
    public List<int> defDebuffValues;
    public List<int> dmgDebuffValues;

    public override void Refresh(int level)
    {
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }
        base.Refresh(level);
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.IncreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for (int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.IncreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }
    }

    public override void Active(int level)
    {
        base.Active(level);
        duration--;
        if(duration <= 0)
        {
            SetIsComplete(true);
        }
    }

    public override void EndBuff()
    {
        base.EndBuff();
        for(int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        }
        for(int i = 0; i < entity.target.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote); ++i)
        {
            entity.CharStat.DecreaseStatBy(dmgDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.receivedDmgIncreaseValue));
        }
    }

    public override void SetIsComplete(bool value)
    {
        entity.CharStat.DecreaseStatBy(defDebuffValues[combineLevel], entity.CharStat.GetStatByType(StatType.armor));
        base.SetIsComplete(value);
    }

    public override void Init()
    {
        base.Init();
        duration = combineLevel + 2;
        EndSkill();
    }

    public void EndSkill()
    {
        Refresh(combineLevel);
    }
}