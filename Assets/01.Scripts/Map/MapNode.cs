using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isStageBubbleReverse;

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
            MapManager.Instanace.SelectStageNimber = value;
        }
    }

    public void ClickThisNode()
    {
        MapManager.Instanace.CreateStageInfoBubble("Sample", transform.localPosition, _isStageBubbleReverse);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickThisNode();
    }
}
