using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private float _fadeWaitTime;
    [SerializeField] private float _fadeOutTime;

    private event Action _fadeOutAction;
    private Sequence _fadeSequence;

    public void HandleProductionDraw(FadeOutType fType)
    {
        _fadeOutAction = null;
        _fadeSequence = DOTween.Sequence();

        switch (fType)
        {
            case FadeOutType.None:
                return;
            case FadeOutType.Normal:
                _fadeOutAction += HandleFadeOutNormal;
                break;
            case FadeOutType.UpToDown:
                _fadeOutAction += HandleFadeOutUpToDown;
                break;
            case FadeOutType.LeftToRight:
                _fadeOutAction += HandleFadeOutLeftToRight;
                break;
            default:
                break;
        }

        _fadeOutAction?.Invoke();
    }

    private void HandleFadeOutNormal()
    {
        _blackPanel.color = _alphaZeroBlackColor;
        _blackPanelTrm.localPosition = Vector3.zero;

        _fadeSequence.Append(_blackPanel.DOFade(1, _fadeInTime));
        _fadeSequence.AppendInterval(_fadeWaitTime);
        _fadeSequence.Append(_blackPanel.DOFade(0, _fadeOutTime));
        _fadeSequence.AppendCallback(() =>
        {
            _blackPanelTrm.localPosition = new Vector2(_blackPanelTrm.sizeDelta.x,
                                                       _blackPanelTrm.sizeDelta.y);
        });
    }

    private void HandleFadeOutUpToDown()
    {
        _blackPanel.color = _blackColor;
        _blackPanelTrm.localPosition = new Vector2(0, _blackPanelTrm.sizeDelta.y);

        _fadeSequence.Append(_blackPanelTrm.DOLocalMoveY(0, _fadeInTime));
        _fadeSequence.AppendInterval(_fadeWaitTime);
        _fadeSequence.Append(_blackPanelTrm.DOLocalMoveY(-_blackPanelTrm.sizeDelta.y, _fadeOutTime));
    }

    private void HandleFadeOutLeftToRight()
    {
        _blackPanel.color = _blackColor;
        _blackPanelTrm.localPosition = new Vector2(-_blackPanelTrm.sizeDelta.x, 0);

        _fadeSequence.Append(_blackPanelTrm.DOLocalMoveX(0, _fadeInTime));
        _fadeSequence.AppendInterval(_fadeWaitTime);
        _fadeSequence.Append(_blackPanelTrm.DOLocalMoveX(_blackPanelTrm.sizeDelta.x, _fadeOutTime));
    }
}
