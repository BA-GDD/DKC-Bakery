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
    private bool _isInRange;

    [SerializeField] private UnityEvent _doughEnterRangeEvent;
    [SerializeField] private UnityEvent _doughExitRangeEvent;
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
            Vector2 myPos = Vector2.Lerp(transform.position, mousePos, Time.deltaTime * 20f);

            if (myPos.x > _stoveMaxRange.x && myPos.y < _stoveMaxRange.y &&
                myPos.x < _stoveMinRange.x && myPos.y > _stoveMinRange.y)
            {
                _isInRange = true;
                _doughEnterRangeEvent?.Invoke();
            }
            else
            {
                _isInRange = false;
                _doughExitRangeEvent?.Invoke();
            }

            _isInnerDough = true;
        }

        if (Input.GetMouseButtonUp(0) && _isInnerDough)
        {
            _isInnerDough = false;
            transform.position = transform.position;
            ActiveInnerStoveRange();
        }
    }
    private void ActiveInnerStoveRange()
    {
        if(_isInRange)
        {
            
        }
        else
        {
            _isInnerDough = false;
            transform.position = _doughNormalPos;
        }
    }    
}
