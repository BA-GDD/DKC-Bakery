using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneUI : SceneUI
{
    [SerializeField]
    private RectTransform _leftStagePanel;
    [SerializeField]
    private RectTransform _leftStageMapPanel;
    [SerializeField]
    private RectTransform _rightStagePanel;
    [SerializeField]
    private RectTransform _rightStageMapPanel;
    [SerializeField]
    private float _stagePanelPositionX;
    [SerializeField]
    private float _stagePanelDuration;
    private string _selectedRegion;

    public void OnRegionSelect(string regionName, RectTransform stageRect)
    {
        if (regionName.Contains("Map"))
        {
            _leftStagePanel.DOAnchorPosX(-_stagePanelPositionX, _stagePanelDuration);
            _rightStagePanel.DOAnchorPosX(_stagePanelPositionX, _stagePanelDuration);

            return;
        }
        else if (stageRect.localPosition.x <= 0)
        {
            for (int i = 0; i < _rightStageMapPanel.childCount; ++i)
            {
                _rightStageMapPanel.GetChild(i).gameObject.SetActive(false);
            }

            _leftStagePanel.DOAnchorPosX(-_stagePanelPositionX, _stagePanelDuration);
            _rightStagePanel.DOAnchorPosX(-_stagePanelPositionX, _stagePanelDuration);
            _rightStageMapPanel.Find($"StageMap_{regionName}").gameObject.SetActive(true);
        }
        else
        {
            Transform stageMapPanel = _leftStagePanel.GetChild(1);

            for (int i = 0; i < _leftStageMapPanel.childCount; ++i)
            {
                _leftStageMapPanel.GetChild(i).gameObject.SetActive(false);
            }

            _rightStagePanel.DOAnchorPosX(_stagePanelPositionX, _stagePanelDuration);
            _leftStagePanel.DOAnchorPosX(_stagePanelPositionX, _stagePanelDuration);
            _leftStageMapPanel.Find($"StageMap_{regionName}").gameObject.SetActive(true);
        }

        _selectedRegion = regionName;

        Debug.Log(_selectedRegion);
    }
}
