using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RegionMapController : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _mapUICamera;
    [SerializeField]
    private Canvas _regionMapCanvas;
    [SerializeField]
    private RectTransform _regionMapPanel;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerPressRaycast.gameObject != gameObject)
        {
            return;
        }

        _regionMapPanel.anchoredPosition += eventData.delta * 0.5f / _regionMapCanvas.scaleFactor;
        _regionMapPanel.anchoredPosition = new Vector3(Mathf.Clamp(_regionMapPanel.anchoredPosition.x, _mapUICamera.pixelRect.xMax - _mainCamera.pixelWidth * 0.45f - _regionMapPanel.sizeDelta.x * 0.5f, _mapUICamera.pixelRect.xMin - _mainCamera.pixelWidth * 0.55f + _regionMapPanel.sizeDelta.x * 0.5f), Mathf.Clamp(_regionMapPanel.anchoredPosition.y, _mapUICamera.pixelRect.yMax - _mainCamera.pixelHeight * 0.45f - _regionMapPanel.sizeDelta.y * 0.5f, _mapUICamera.pixelRect.yMin - _mainCamera.pixelHeight * 0.55f + _regionMapPanel.sizeDelta.y * 0.5f), 0f);
    }
}
