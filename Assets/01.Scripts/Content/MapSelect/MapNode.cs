using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isStageBubbleReverse;
    [SerializeField] private StageDataSO _stageData;

    private int _stageNumber;
    public int StageNumber
    {
        get
        {
            return _stageNumber;
        }
        set
        {
            _stageNumber = value;
            MapManager.Instanace.SelectStageNumber = value;
        }
    }

    public void ClickThisNode()
    {
        MapManager.Instanace.CreateStageInfoBubble(_stageData.stageName, 
                                                   transform.localPosition, 
                                                   _isStageBubbleReverse);
        MapManager.Instanace.SelectStageData = _stageData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickThisNode();
    }
}
