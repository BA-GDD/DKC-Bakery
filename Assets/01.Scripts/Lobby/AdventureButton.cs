using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIDefine;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdventureButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _visualTrm;
    [SerializeField] private GameObject _selectAdventureType;
    private Tween _hoverTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverTween.Kill();
        _hoverTween = _visualTrm.DOScale(Vector2.one * 1.2f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverTween.Kill();
        _hoverTween = _visualTrm.DOScale(Vector2.one, 0.1f);
    }

    private void OnDisable()
    {
        _hoverTween.Kill();
    }

    public void PressButton()
    {
        _selectAdventureType.SetActive(true);
    }
}