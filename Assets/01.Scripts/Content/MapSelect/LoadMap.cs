using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private Transform _nodeMapParent;
    private MapNode[] _mapNodeArr;

    private void Start()
    {
        _mapNodeArr = _nodeMapParent.GetComponentsInChildren<MapNode>();
        AdventureData adData = UIManager.Instance.GetSceneUI<SelectMapUI>().GetAdventureData();

        int chapterIdx = (int)MapManager.Instanace.SelectMapData.myChapterType;
        bool[] canChallingingStageArr = adData.canChallingeChapterArr[chapterIdx];

        for (int i = 0; i < canChallingingStageArr.Length; i++)
        {
            _mapNodeArr[i].gameObject.SetActive(canChallingingStageArr[i]);
        }
    }
}
