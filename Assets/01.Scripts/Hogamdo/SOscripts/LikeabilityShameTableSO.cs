using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(menuName = "SO/Hogam/LikeabilityShameTable")]
public class LikeabilityShameTableSO : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<string> ID = new List<string>();
    public List<LikeabilityStandard> DataList = new List<LikeabilityStandard>();
    public void UpdateStats(List<GSTU_Cell> list)
    {
        List<string> datas = new List<string>();
        for (int i = 1; i < 3; i++)
        {
            datas.Add(list[i].value);
        }
        LikeabilityStandard sendData = new LikeabilityStandard(datas);
        DataList.Add(sendData);
    }
}

[CustomEditor(typeof(LikeabilityShameTableSO))]
public class DataEditor : Editor
{
    LikeabilityShameTableSO data;

    void OnEnable()
    {
        data = (LikeabilityShameTableSO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        foreach (string dataName in data.ID)
            data.UpdateStats(ss.rows[dataName]);
        EditorUtility.SetDirty(target);
    }
}
