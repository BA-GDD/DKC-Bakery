using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;
using UnityEditor;

[CreateAssetMenu(menuName ="SO/CardUpgrade/info")]
public class CardUpgradeInfo : LoadableData
{
    
}

[CustomEditor(typeof(CardUpgradeInfo))]
public class CardUpgradeInfoDataLoader : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardUpgradeInfo cardUpgradeInfo = (CardUpgradeInfo)target;
        LoadableData ld = cardUpgradeInfo;

        if (GUILayout.Button("CardUpgradeInfoDataReading"))
        {
            ld.Generate();
        }
    }
}

