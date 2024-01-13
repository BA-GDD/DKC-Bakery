using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SO/Ba")]
public class Bakery : LoadableData
{
    [CustomEditor(typeof(Bakery))]
    public class EpisodeLoader : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Bakery episodeData = (Bakery)target;
            LoadableData ld = episodeData;
            
            if (GUILayout.Button("DataGenerate"))
            {
                Debug.Log("DataGenerate Start . . .");
                ld.Generate();
            }
        }
    }
}
