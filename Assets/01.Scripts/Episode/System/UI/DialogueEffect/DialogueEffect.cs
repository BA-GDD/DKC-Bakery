using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DialogueEffect : MonoBehaviour
{
    [SerializeField] private Image _bubble;
    [SerializeField] private Image _effectElement;
    [SerializeField] private float _fadingTime;

    [SerializeField] private AnimatorOverrideController _effectAnimator;
    [SerializeField] private Animator _animator;
    private readonly int _startFxHash = Animator.StringToHash("");

    public void StartEffect(Sprite img, AnimationClip clip)
    {
        _effectElement.sprite = img;

        Sequence seq = DOTween.Sequence();
        seq.Append(_bubble.DOFade(1, _fadingTime));
        seq.Join(_effectElement.DOFade(1, _fadingTime));
        seq.AppendCallback(() =>
        {
            _effectAnimator[""] = clip;
            _animator.SetBool(_startFxHash, true);
        });
    }

    public void EndEffect()
    {

    }
}
