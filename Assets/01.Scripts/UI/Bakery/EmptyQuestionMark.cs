using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmptyQuestionMark : MonoBehaviour
{
    private void Start()
    {
        float currentY = transform.localPosition.y;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(currentY + 20, 1));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DOLocalMoveY(currentY, 1));
        seq.AppendInterval(0.1f);
        seq.SetLoops(-1);
    }
}
