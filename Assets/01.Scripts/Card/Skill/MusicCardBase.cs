using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicCardBase : CardBase
{
    protected int GetNoteCount()
    {
        int noteCnt = 0;
        noteCnt = Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote) + Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote) + Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote);
        return noteCnt;
    }

    protected bool HasDEFMusicalNoteStack()
    {
        return Player.BuffStatCompo.GetStack(StackEnum.DEFMusicalNote) > 0;
    }

    protected bool HasDMGMusicalNoteStack()
    {
        return Player.BuffStatCompo.GetStack(StackEnum.DMGMusicaldNote) > 0;
    }

    protected bool HasFAINTMusicalNoteStack()
    {
        return Player.BuffStatCompo.GetStack(StackEnum.FAINTMusicalNote) > 0;
    }

    protected void ApplyDebuffToAllEnemy()
    {
        //if (HasDEFMusicalNoteStack())
        //{
        //    foreach (var e in Player.GetSkillTargetEnemyList[this])
        //    {
        //        e?.BuffStatCompo.AddBuff(buffSO, buffSO.stackBuffs[1].values[(int)CombineLevel], (int)CombineLevel);
        //    }
        //}

    }
}
