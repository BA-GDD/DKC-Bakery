using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEditor;

[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikebilityData : LoadableData
{
    public int NeedShameToLevelUp(int level)
    {
        return Convert.ToInt32(generateData[level].str[1]);
    }
}

[CustomEditor(typeof(LikebilityData))]
public class LikebilityDataLoader : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LikebilityData likebilityData = (LikebilityData)target;
        LoadableData ld = likebilityData;
        
        if (GUILayout.Button("LikebilityDataReading"))
        {
            ld.Generate();
        }
    }
}


