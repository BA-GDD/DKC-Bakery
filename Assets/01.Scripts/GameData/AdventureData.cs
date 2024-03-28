using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureData : CanSaveData
{
    public string InChallingingMissionName = "-";

    public string ChallingingMineFloor = "1";

    public bool[][] canChallingeChapterArr =
    {
        new bool[6] { true, false, false, false, false, false},
        new bool[6] { false, false, false, false, false, false},
        new bool[6] { false, false, false, false, false, false},
        new bool[6] { false, false, false, false, false, false},
        new bool[6] { false, false, false, false, false, false},
        new bool[6] { false, false, false, false, false, false},
    };
    public bool[] IsLookUnLockProductionArr = new bool[6];
    public string InChallingingStageCount = "1-1";

    public override void SetInitialValue()
    {

    }
}
