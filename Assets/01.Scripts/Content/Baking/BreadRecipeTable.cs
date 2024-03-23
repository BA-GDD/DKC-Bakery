using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SO/BreadRecipeTable")]
public class BreadRecipeTable : LoadableData
{
    private bool _isMatch = false;

    public ItemDataBreadSO Bake(string[] ingredientNames)
    {
        _isMatch = true;
        ItemDataBreadSO returnBread = null;

        for(int i = 1; i < generateData.Count; ++i)
        {
            for(int j = 1; j < generateData[i].str.Length; ++j)
            {
                if(ingredientNames[j - 1] != generateData[i].str[j])
                {
                    _isMatch = false;
                    break;
                }
            }

            if (_isMatch)
            {
                // 매치되는 경우가 있을 경우 해당 것 반환해줘야함
                returnBread = BakingManager.Instance.breadDictionary[generateData[i].str[0]];
                return returnBread;
            }

            _isMatch = true;
        }

        if(returnBread == null)
        {
            returnBread = BakingManager.Instance.breadDictionary["DubiousBread"];
        }

        return returnBread;
    }

    [CustomEditor(typeof(BreadRecipeTable))]
    public class EpisodeLoader : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BreadRecipeTable episodeData = (BreadRecipeTable)target;
            LoadableData ld = episodeData;

            if (GUILayout.Button("DataGenerate"))
            {
                Debug.Log("DataGenerate Start . . .");
                ld.Generate();
            }
        }
    }
}
