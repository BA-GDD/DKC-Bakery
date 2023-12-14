using System.Collections;                                                                                                                   
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataBase))]
public class DataLoader : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DataBase dataBase = (DataBase)target;
        if(GUILayout.Button("Data Generate"))
        {
            dataBase.GenerateDatas();
        }
    }
}
