using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionMapEventTrigger : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        (UIManager.Instance.currentSceneUI as MapSceneUI).OnDrag(eventData);
    }
}
