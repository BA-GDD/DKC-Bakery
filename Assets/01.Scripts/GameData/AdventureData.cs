using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureData : CanSaveData
{
    public string InChallingingMissionName = "-";

    public string ChallingingMineFloor = "1";

    public bool[] IsLookUnLockProductionArr = new bool[6];
    public string InChallingingStageCount = "1-1";

    public void SerializeArray()
    {

    }

    public override void SetInitialValue()
    {

    }
}
