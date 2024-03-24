using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class DamageText : PoolableMono
{
    [SerializeField] private Vector3 _moveOffset;
    [SerializeField] private Vector2 _reactionMinOffset;
    [SerializeField] private Vector2 _reactionMaxOffset;
    private TextMeshPro _tmpText;
    public TextMeshPro TmpText
    {
        get
        {
            if(_tmpText != null) return _tmpText; 
            _tmpText = GetComponent<TextMeshPro>();
            return _tmpText;
        }
    }

    public void ShowDamageText(Vector3 position, int damage, float fontSize, Color color)
    {
        TmpText.color = color;
        TmpText.fontSize = fontSize;
        TmpText.text = damage.ToString();

        position.z = -5;
        transform.position = position;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + _moveOffset, 0.7f));
        seq.Join(TmpText.DOFade(0, 1f));
        seq.OnComplete(() => PoolManager.Instance.Push(this));
    }

    public void ShowReactionText(Vector3 position, string word, float fontSize, Color color)
    {
        TmpText.color = color;
        TmpText.fontSize = fontSize;
        TmpText.text = word;

        position.z = -5;
        transform.position = position;

        Vector3 randomPos =
                new Vector2(Random.Range(_reactionMinOffset.x, _reactionMaxOffset.y),
                            Random.Range(_reactionMinOffset.y, _reactionMaxOffset.y));

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + randomPos, 0.7f));
        seq.Join(TmpText.DOFade(0, 1f));
        seq.OnComplete(() => PoolManager.Instance.Push(this));
    }

    public override void Init()
    {
        TmpText.color = Color.white;
    }
}
