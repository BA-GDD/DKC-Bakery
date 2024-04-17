using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MazeDoor : MonoBehaviour, IPointerEnterHandler, 
                                       IPointerExitHandler,
                                       IPointerClickHandler
{
    [SerializeField] private Sprite _sample;
    public MyosuTestInfo AssignedMyosuInfo { get; set; }
    private CanvasGroup _visual;
    public CanvasGroup Visual
    {
        get
        {
            if(_visual == null)
            {
                _visual = GetComponent<CanvasGroup>();
            }
            return _visual;
        }
    }
    [SerializeField] private Transform _doorTrm;
    [SerializeField] private CompensationBubble _comBubble;
    [SerializeField] private UnityEvent<MazeDoor> _doorHoverEvent;
    [SerializeField] private UnityEvent<MazeDoor> _doorHoverOutEvent;

    private Vector3 _normalScale;
    private Tween _hoverTween;
    private Tween _shakeTween;

    private void Start()
    {
        _normalScale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoverTween.Kill();

        _hoverTween = transform.DOScale(transform.localScale * 1.1f, 0.3f);
        _shakeTween = transform.DOShakeRotation(1f, 3, 10).SetLoops(-1);
        _comBubble.SpeachUpBubble(_sample, 50);

        _doorHoverEvent?.Invoke(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _hoverTween.Kill();
        _shakeTween.Kill();

        transform.rotation = Quaternion.identity;
        _hoverTween = transform.DOScale(_normalScale, 0.3f);
        _comBubble.SpeachDownBubble();

        _doorHoverOutEvent?.Invoke(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
