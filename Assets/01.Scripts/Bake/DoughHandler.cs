using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoughHandler : MonoBehaviour
{
    [Header("StoveRange")]
    [SerializeField] private Vector2 _stoveMaxRange;
    [SerializeField] private Vector2 _stoveMinRange;
    [SerializeField] private Vector2 _stoveEnterPos;

    [Space(10)]

    private Vector2 _doughNormalPos;
    private bool _isInnerDough;

    [SerializeField] private UnityEvent _doughDragStartEvent;
    [SerializeField] private UnityEvent _doughToInnerEndEvent;

    void Start()
    {
        _doughNormalPos = transform.position;
    }
    private void OnMouseEnter()
    {
        _isInnerDough = true;
    }
    private void OnMouseExit()
    {
        _isInnerDough = false;
    }
    void Update()
    {
        ActiveCheck();
    }
    private void ActiveCheck()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePos, Time.deltaTime * 20f);

            _isInnerDough = true;
        }
        else if (Input.GetMouseButtonUp(0) && _isInnerDough)
        {
            _isInnerDough = false;
            transform.position = transform.position;
            ActiveInnerStoveRange();
        }
    }
    private void ActiveInnerStoveRange()
    {
        Vector2 myPos = transform.position;

        if(myPos.x > _stoveMaxRange.x && myPos.y < _stoveMaxRange.y && 
           myPos.x < _stoveMinRange.x && myPos.y > _stoveMinRange.y)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMoveX(_stoveEnterPos.x, 0.5f).SetEase(Ease.OutQuad));
            seq.Join(transform.DOMoveY(_stoveEnterPos.y, 0.5f).SetEase(Ease.InQuad));
            seq.Join(transform.DOScale(Vector2.zero, 0.5f).SetEase(Ease.OutBack));
            seq.AppendCallback(() => _doughToInnerEndEvent?.Invoke());
        }
        else
        {
            _isInnerDough = false;
            transform.position = _doughNormalPos;
        }
    }    
}
