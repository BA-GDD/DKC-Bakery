using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ChapterElement : MonoBehaviour
{
    [SerializeField] private MapDataSO _chapterData;

    [Header("ÂüÁ¶°ª")]
    [SerializeField] private TextMeshProUGUI _chapterNameTxt;
    [SerializeField] private TextMeshProUGUI _chapterInfoTxt;
    [SerializeField] private GameObject _lockPanel;

    private void Start()
    {
        _chapterNameTxt.text = _chapterData.chapterName;
        _chapterInfoTxt.text = _chapterData.chapterInfo;
    }

    public void SelectThisChapter()
    {
        MapManager.Instanace.SelectMapData = _chapterData;
        MapManager.Instanace.ActiveLoadMap(true);
    }
}
