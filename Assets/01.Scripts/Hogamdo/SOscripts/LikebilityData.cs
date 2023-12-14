using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikebilityData : LoadableData
{
    public List<string[]> LikebilityShame => base.generateData;

    public int NeedShameToLevelUp(int level)
    {
        return Convert.ToInt32(LikebilityShame[level][1]);
    }
}

