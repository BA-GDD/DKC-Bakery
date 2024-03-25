using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public struct HeartEffectEntity
{
    public Transform heartTrm;
    public Sprite heartSprite;
    public AnimationClip clip;
}

public class EatRange : MonoBehaviour
{
    private RectTransform _standRect;

    [SerializeField] private RectTransform _rangeRect;
    [SerializeField] private Image _characterStand;
    [SerializeField] private Sprite _openMouseSprite;
    [SerializeField] private Sprite _normalMouseSprite;

    [SerializeField] private HeartEffectEntity _heartEntity;

    [Header("µð¹ö±×")]
    [SerializeField] private bool _canEat;
    [field: SerializeField] public bool IsHoldingCake { get; set; }

    private void Awake()
    {
        _standRect = _characterStand.GetComponent<RectTransform>();
        _canEat = true;
    }

    private void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(_rangeRect, Input.mousePosition))
        {
            OnPointerEnter();
        }
        else
        {
            OnPointerExit();
        }
    }

    public void OnPointerEnter()
    {
        if (!_canEat || !IsHoldingCake) return;

        _characterStand.sprite = _openMouseSprite;
    }

    public void OnPointerExit()
    {
        if (_canEat || !IsHoldingCake) return;

        _characterStand.sprite = _normalMouseSprite;
    }

    public void OnPointerUp()
    {
        if (!_canEat) return;

        _canEat = false;

        MakeHeartOicyEffect();

        Sequence seq = DOTween.Sequence();
        seq.Append(_standRect.DOLocalJump(new Vector3(40, -163), 50, 1, 0.5f));
        seq.AppendCallback(() =>
        {
            _characterStand.sprite = _normalMouseSprite;
            _canEat = true;
        });
    }

    private void MakeHeartOicyEffect()
    {
        DialogueEffect de = PoolManager.Instance.Pop(PoolingType.DialogueEffect) as DialogueEffect;
        de.transform.SetParent(_heartEntity.heartTrm);
        de.transform.localScale = Vector3.one;
        de.transform.localPosition = Vector3.zero;
        de.StartEffect(_heartEntity.heartSprite, _heartEntity.clip, EpisodeDialogueDefine.MoveType.GoCenter);
    }
}
