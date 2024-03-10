using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChapterDefine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ChapterInfoPanel : MonoBehaviour
{
    [Header("패널")]
    [SerializeField] private Transform _panelTrm;

    [Header("셋팅값")]
    [SerializeField] private float _activeTime;

    [Header("참조값")]
    [SerializeField] private TextMeshProUGUI _chapterTypeText;
    [SerializeField] private TextMeshProUGUI _chapterNameText;
    [SerializeField] private Image _chapterImage;
    [SerializeField] private TextMeshProUGUI _chapterInfoText;
    [SerializeField] private Transform _loadMapParentTrm;
    private NodeLaodMap _loadMapPrefab;
    private NodeLaodMap _loadMapObject;

    public void SetInfo(MapDataSO mapData)
    {
        _chapterTypeText.text = mapData.myChapterType.ToString().ToUpper();
        _chapterNameText.text = mapData.chapterName;
        _chapterImage.sprite = mapData.chapterSprite;
        _chapterInfoText.text = mapData.chapterInfo;
        _loadMapPrefab = mapData.loadMap;
    }

    public void ActivePanel(bool isActive)
    {
        MapManager.Instanace.isOnPanel = isActive;
        _panelTrm.DOLocalMoveX(Convert.ToInt32(!isActive) * 550, _activeTime);
    }

    public void ActiveLoadMap(bool isActive)
    {
        MapManager.Instanace.isOnLoadMap = isActive;
        if(isActive)
        {
            _loadMapObject = Instantiate(_loadMapPrefab, _loadMapParentTrm);
        }
        else
        {
            Destroy(_loadMapObject.gameObject);
        }
    }

    public NodeLaodMap GetNodeLoadMap()
    {
        if(_loadMapObject != null) return _loadMapObject;
        Debug.LogError("Something is wrong. You call LoadMapObject before create");
        return null;
    }
}
