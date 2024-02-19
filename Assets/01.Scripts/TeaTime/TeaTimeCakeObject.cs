using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TeaTimeCakeObject : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public bool CanCollocate { get; set; }
    [SerializeField] private Image _cakeImg;
    [SerializeField] private EatRange _eatRange;
    private Vector2 _usuallyPos;

    private void Awake()
    {
        //_cakeImg.enabled = false;
        _usuallyPos = transform.position;
    }
    public void SetCakeImage(Sprite cakeSprite)
    {
        _cakeImg.sprite = cakeSprite;
        _cakeImg.enabled = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _usuallyPos;

        _eatRange.IsHoldingCake = false;
        _eatRange.OnPointerUp();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        _eatRange.IsHoldingCake = true;
    }
}
