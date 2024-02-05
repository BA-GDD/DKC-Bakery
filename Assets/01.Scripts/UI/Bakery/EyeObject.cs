using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EyeObject : MonoBehaviour
{
    [SerializeField] private float _easingTime = 0.2f;
    [SerializeField] private float _intervalTime;
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(0, _easingTime));
        seq.Append(transform.DOScaleY(1, _easingTime));
        seq.AppendInterval(_intervalTime);

        seq.SetLoops(-1);
    }
}
