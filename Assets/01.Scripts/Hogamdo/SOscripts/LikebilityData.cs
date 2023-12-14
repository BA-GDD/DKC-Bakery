using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEditor;


[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikebilityData : LoadableData
{
    public string[] likebilityShameData;
    public override void AddData(string[] dataArr)
    {
        likebilityShameData = dataArr;
    }
}