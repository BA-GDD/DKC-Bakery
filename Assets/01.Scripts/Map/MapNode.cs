using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isStageBubbleReverse;
    public void ClickThisNode()
    {
        MapManager.Instanace.CreateStageInfoBubble("Sample", transform.localPosition, _isStageBubbleReverse);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickThisNode();
    }
}
