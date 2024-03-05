using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName ="SO/Stage")]
public class StageInfoSO : LoadableData
{
    [CustomEditor(typeof(StageInfoSO))]
    public class StageInfoLoader : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            StageInfoSO episodeData = (StageInfoSO)target;
            LoadableData ld = episodeData;
            if (GUILayout.Button("EpisodeDataReading"))
            {
                Debug.Log("DataReading Start . . .");
                //ld.GeneratDialogueData();
            }
            if (GUILayout.Button("DataGenerate"))
            {
                Debug.Log("DataGenerate Start . . .");
                ld.Generate();
            }
        }
    }

    public List<Data> datas = new List<Data>();

    public void GetList()
    {
        datas = generateData;
    }
}
