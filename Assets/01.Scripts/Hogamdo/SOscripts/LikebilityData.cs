using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikebilityData : LoadableData
{
    public int NeedShameToLevelUp(int level)
    {
        return Convert.ToInt32(generateData[level].rowData[1]);
    }
}

