using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage : MonoBehaviour, IPointerDownHandler
{
    private RectTransform _rectangleTransform;

    private void Awake()
    {
        _rectangleTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        (UIManager.Instance.currentSceneUI as MapSceneUI).OnRegionSelect(name.Replace("Region_", ""), _rectangleTransform);
    }
}
