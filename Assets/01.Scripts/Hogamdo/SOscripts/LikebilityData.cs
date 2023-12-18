using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikebilityData : LoadableData
{
    public int NeedShameToLevelUp(int level)
    {
        Debug.Log(generateData[level][1]);
        return Convert.ToInt32(generateData[level][1]);
    }
}

