using UnityEngine;
using System.Collections.Generic;
using EpisodeDialogueDefine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class EpisodeProductionDrawer : MonoBehaviour
{
    [Header("블랙 페널")]
    [SerializeField] private Image _blackPanel;
    [SerializeField] private RectTransform _blackPanelTrm;
    [SerializeField] private Color _blackColor;
    [SerializeField] private Color _alphaZeroBlackColor;

    [Header("수치 조정")]
    [SerializeField] private float _fadeInTime;
    [SerializeField] private float _fadeOutTime;

    private event Action _fadeInOutAction;
    private FadeOutType _beforeFadeOutType;
    private Dictionary<FadeOutType, Action> _findFadeAction = new Dictionary<FadeOutType, Action>();

    private void OnEnable()
    {
        _findFadeAction = new Dictionary<FadeOutType, Action>();
        foreach(FadeOutType ft in Enum.GetValues(typeof(FadeOutType)))
        {
            switch (ft)
            {
                case FadeOutType.None:
                    _findFadeAction.Add(FadeOutType.None, HandleFadeOut);
                    break;
                case FadeOutType.Normal:
                    _findFadeAction.Add(FadeOutType.Normal, HandleFadeInNormal);
                    break;
                case FadeOutType.UpToDown:
                    _findFadeAction.Add(FadeOutType.UpToDown, HandleFadeInUpToDown);
                    break;
                case FadeOutType.LeftToRight:
                    _findFadeAction.Add(FadeOutType.LeftToRight, HandleFadeInLeftToRight);
                    break;
                default:
                    break;
            }
        }
    }

    public void HandleProductionDraw(FadeOutType fType)
    {
        _fadeInOutAction = null;

        _fadeInOutAction += _findFadeAction[fType];

        _fadeInOutAction?.Invoke();
        _beforeFadeOutType = fType;
    }

    private void HandleFadeOut()
    {
        switch (_beforeFadeOutType)
        {
            case FadeOutType.None:
                break;
            case FadeOutType.Normal:
                Sequence _fadeSequence = DOTween.Sequence();
                _fadeSequence.Append(_blackPanel.DOFade(0, _fadeOutTime));
                _fadeSequence.AppendCallback(() =>
                {
                    _blackPanelTrm.localPosition = new Vector2(_blackPanelTrm.sizeDelta.x,
                                                               _blackPanelTrm.sizeDelta.y);
                });
                break;
            case FadeOutType.UpToDown:
                _blackPanelTrm.DOLocalMoveY(-_blackPanelTrm.sizeDelta.y, _fadeOutTime);
                break;
            case FadeOutType.LeftToRight:
                _blackPanelTrm.DOLocalMoveX(_blackPanelTrm.sizeDelta.x, _fadeOutTime);
                break;
        }
    }

    private void HandleFadeInNormal()
    {
        _blackPanel.color = _alphaZeroBlackColor;
        _blackPanelTrm.localPosition = Vector3.zero;

        _blackPanel.DOFade(1, _fadeInTime);
    }

    private void HandleFadeInUpToDown()
    {
        _blackPanel.color = _blackColor;
        _blackPanelTrm.localPosition = new Vector2(0, _blackPanelTrm.sizeDelta.y);

        _blackPanelTrm.DOLocalMoveY(0, _fadeInTime);
        
    }

    private void HandleFadeInLeftToRight()
    {
        _blackPanel.color = _blackColor;
        _blackPanelTrm.localPosition = new Vector2(-_blackPanelTrm.sizeDelta.x, 0);

        _blackPanelTrm.DOLocalMoveX(0, _fadeInTime);
    }
}
