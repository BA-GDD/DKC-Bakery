using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage : MonoBehaviour, IPointerDownHandler
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        name = name.Replace("Stage", "");
        name = name.Insert(0, "Region_");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        (UIManager.Instance.currentSceneUI as MapSceneUI).OnRegionSelect(name.Replace("Region_", ""), rectTransform);
    }
}
