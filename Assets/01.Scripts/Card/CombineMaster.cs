using CardDefine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMaster : MonoBehaviour
{
    [SerializeField] private GameObject _combineFX;
    [SerializeField] private float _combinningSce;
    [SerializeField] private float _combineCompleteSec;

    public void CombineCard(CardBase cb_1, CardBase cb_2)
    {
        Debug.Log("CombineStart");

        float combineXPos = (cb_1.transform.localPosition.x + cb_2.transform.localPosition.x) * 0.5f;
        float targeXtPos = cb_2.transform.localPosition.x;
        Vector2 visualNormalPos = cb_1.VisualTrm.transform.localPosition;

        Sequence seq = DOTween.Sequence();
        seq.Append(cb_1.transform.DOLocalMoveX(combineXPos, _combinningSce).SetEase(Ease.InCubic));
        seq.Join(cb_2.transform.DOLocalMoveX(combineXPos, _combinningSce).SetEase(Ease.InCubic));
        seq.Join(cb_1.VisualTrm.DOShakePosition(_combinningSce, 3, 30));
        seq.Join(cb_2.VisualTrm.DOShakePosition(_combinningSce, 3, 30));
        seq.AppendCallback(() =>
        {
            CardReader.RemoveCardInHand(cb_1);
            Destroy(cb_1.gameObject);

            cb_2.CombineLevel = (cb_2.CombineLevel + 1);
        });
        seq.Append(cb_2.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 359, 0), _combinningSce));
        seq.Append(cb_2.transform.DOLocalMoveX(targeXtPos, _combinningSce));
        seq.AppendCallback(() => CardReader.CardDrawer.CanDraw = true);
    }
}
