using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableRegion : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public Canvas orderCanvas;
    [HideInInspector]
    public RectTransform rectangleTransform;
    [HideInInspector]
    public string regionName;
    [SerializeField]
    private bool _isRegion;

    private void Awake()
    {
        orderCanvas = GetComponent<Canvas>();
        rectangleTransform = GetComponent<RectTransform>();
        regionName = name.Replace("Region_", "");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isRegion)
        {
            (UIManager.Instance.currentSceneUI as MapSceneUI).OnRegionSelect(this);
        }
        else
        {
            (UIManager.Instance.currentSceneUI as MapSceneUI).OnRegionDeselect();
        }
    }
}
