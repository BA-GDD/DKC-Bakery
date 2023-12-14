using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneUI : SceneUI
{
    [SerializeField]
    private RectTransform leftStagePanel;
    [SerializeField]
    private RectTransform rightStagePanel;
    [SerializeField]
    private float stagePanelPositionX;
    [SerializeField]
    private float stagePanelDuration;
    private string selectedRegion;

    public void OnRegionSelect(string stageName, RectTransform stageRect)
    {
        if (stageName.Contains("Map"))
        {
            leftStagePanel.DOAnchorPosX(-stagePanelPositionX, stagePanelDuration);
            rightStagePanel.DOAnchorPosX(stagePanelPositionX, stagePanelDuration);

            return;
        }
        else if (stageRect.localPosition.x <= 0)
        {
            leftStagePanel.DOAnchorPosX(-stagePanelPositionX, stagePanelDuration);
            rightStagePanel.DOAnchorPosX(-stagePanelPositionX, stagePanelDuration);
        }
        else
        {
            rightStagePanel.DOAnchorPosX(stagePanelPositionX, stagePanelDuration);
            leftStagePanel.DOAnchorPosX(stagePanelPositionX, stagePanelDuration);
        }

        selectedRegion = stageName;
        Debug.Log(selectedRegion);
    }
}
