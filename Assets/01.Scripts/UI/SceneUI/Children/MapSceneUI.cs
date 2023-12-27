using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneUI : SceneUI
{
    [SerializeField]
    private RectTransform _stageMapBackgroundPanel;
    [SerializeField]
    private RectTransform _stageMapPanel;
    private SelectableRegion _selectedRegion;

    public void OnRegionDeselect()
    {
        //_stageMapPanel.Find($"StageMap_{_selectedRegion.regionName}").gameObject.SetActive(false);
        _stageMapPanel.gameObject.SetActive(false);

        _selectedRegion.orderCanvas.sortingOrder = -5;
        _selectedRegion = null;

        _stageMapBackgroundPanel.gameObject.SetActive(false);
    }

    public void OnRegionSelect(SelectableRegion region)
    {
        _stageMapBackgroundPanel.gameObject.SetActive(true);

        region.orderCanvas.sortingOrder = 5;

        _stageMapPanel.gameObject.SetActive(true);
        //_stageMapPanel.Find($"StageMap_{region.regionName}").gameObject.SetActive(true);

        _selectedRegion = region;

        Debug.Log(_selectedRegion.name);
    }
}
