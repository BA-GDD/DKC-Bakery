using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSceneUI : SceneUI
{
    [Header("Region Map")]
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _mapUICamera;
    [SerializeField]
    private Canvas _regionMapCanvas;
    [SerializeField]
    private RectTransform _regionMapPanel;
    [SerializeField]
    private float _dragSpeed, _zoomSpeed, _minimumZoomMultiflier, _maximumZoomMutiflier;
    private Vector3 _originRegionMapPanelAnchoredPosition;
    private Sequence _sequence;
    private float _zoomMultiflier;
    private float _originZoomMultiflier;

    [Header("Region Select")]
    [SerializeField]
    private RectTransform _stageMapBackgroundPanel;
    [SerializeField]
    private RectTransform _stageMapPanel;
    private SelectableRegion _selectedRegion;

    private void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            _zoomMultiflier -= _zoomSpeed * Time.deltaTime;

            OnZoom();
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            _zoomMultiflier += _zoomSpeed * Time.deltaTime;

            OnZoom();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerPressRaycast.gameObject != _regionMapPanel.gameObject)
        {
            return;
        }

        _regionMapPanel.anchoredPosition += eventData.delta * _dragSpeed / _regionMapCanvas.scaleFactor;
        _regionMapPanel.anchoredPosition = new Vector3(Mathf.Clamp(_regionMapPanel.anchoredPosition.x, _mapUICamera.pixelRect.xMax - _mainCamera.pixelWidth * 0.45f - _regionMapPanel.sizeDelta.x * _regionMapPanel.localScale.x * 0.5f, _mapUICamera.pixelRect.xMin - _mainCamera.pixelWidth * 0.55f + _regionMapPanel.sizeDelta.x * _regionMapPanel.localScale.x * 0.5f), Mathf.Clamp(_regionMapPanel.anchoredPosition.y, _mapUICamera.pixelRect.yMax - _mainCamera.pixelHeight * 0.45f - _regionMapPanel.sizeDelta.y * _regionMapPanel.localScale.y * 0.5f, _mapUICamera.pixelRect.yMin - _mainCamera.pixelHeight * 0.55f + _regionMapPanel.sizeDelta.y * _regionMapPanel.localScale.y * 0.5f), 0f);
    }

    public void OnRegionDeselect()
    {
        _stageMapPanel.Find($"StageMap_{_selectedRegion.regionName}").gameObject.SetActive(false);
        _stageMapPanel.gameObject.SetActive(false);

        _selectedRegion.orderCanvas.sortingOrder = -5;
        _selectedRegion = null;

        _stageMapBackgroundPanel.gameObject.SetActive(false);
    }

    public void OnRegionSelect(SelectableRegion region)
    {
        _stageMapBackgroundPanel.gameObject.SetActive(true);

        region.orderCanvas.sortingOrder = 5;
        _sequence = DOTween.Sequence();

        _sequence
            .OnStart(() =>
            {
                _originRegionMapPanelAnchoredPosition = _regionMapPanel.anchoredPosition;
                _originZoomMultiflier = _zoomMultiflier;
            })
            .Append(_regionMapPanel.DOAnchorPos(region.rectangleTransform.anchoredPosition * -1 * _maximumZoomMutiflier - new Vector2((_mainCamera.pixelWidth - _stageMapPanel.sizeDelta.x) * 0.5f, 0f), 0.25f)) // <= 여기 수정하기
            .Join(DOTween.To(() => _zoomMultiflier, x => _zoomMultiflier = x, _maximumZoomMutiflier, 0.25f))
            .OnUpdate(() =>
            {
                OnZoom();
            });

        _stageMapPanel.gameObject.SetActive(true);
        _stageMapPanel.Find($"StageMap_{region.regionName}").gameObject.SetActive(true);

        _selectedRegion = region;

        Debug.Log(_selectedRegion.regionName);
    }

    private void OnZoom()
    {
        _zoomMultiflier = Mathf.Clamp(_zoomMultiflier, _minimumZoomMultiflier, _maximumZoomMutiflier);
        _regionMapPanel.localScale = Vector3.one * _zoomMultiflier;
        _regionMapPanel.anchoredPosition = new Vector3(Mathf.Clamp(_regionMapPanel.anchoredPosition.x, _mapUICamera.pixelRect.xMax - _mainCamera.pixelWidth * 0.45f - _regionMapPanel.sizeDelta.x * _regionMapPanel.localScale.x * 0.5f, _mapUICamera.pixelRect.xMin - _mainCamera.pixelWidth * 0.55f + _regionMapPanel.sizeDelta.x * _regionMapPanel.localScale.x * 0.5f), Mathf.Clamp(_regionMapPanel.anchoredPosition.y, _mapUICamera.pixelRect.yMax - _mainCamera.pixelHeight * 0.45f - _regionMapPanel.sizeDelta.y * _regionMapPanel.localScale.y * 0.5f, _mapUICamera.pixelRect.yMin - _mainCamera.pixelHeight * 0.55f + _regionMapPanel.sizeDelta.y * _regionMapPanel.localScale.y * 0.5f), 0f);
    }
}
