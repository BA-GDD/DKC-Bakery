using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/PlayerDie")]
public class PlayerDieCondition : TsumegoCondition
{
    public override bool CheckCondition()
    {
        return true;
    }
}
