using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class DamageText : PoolableMono
{
    [SerializeField] private Vector3 _moveOffset;
    private TextMeshPro _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TextMeshPro>();
    }

    public void ShowDamageText(Vector3 position, int damage, float fontSize, Color color)
    {

        _tmpText.color = color;
        _tmpText.fontSize = fontSize;
        _tmpText.text = damage.ToString();

        transform.position = position;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(transform.position + _moveOffset, 0.7f));
        seq.Join(_tmpText.DOFade(0, 1f));
        seq.OnComplete(() => PoolManager.Instance.Push(this));
    }

    public override void Init()
    {
        _tmpText.color = Color.white;
    }
}
