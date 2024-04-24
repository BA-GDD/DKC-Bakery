using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookRecipePreviewPanel : PreviewPanel
{
    [SerializeField] private Vector2 _crookedAngleRange;
    [SerializeField] private Image _cakeImage;
    [SerializeField] private TextMeshProUGUI _cakeNameText;
    [SerializeField] private Transform _decisionBtn;

    protected override void LookUpContent()
    {
        ItemDataBreadSO cakeInfo = BakingManager.Instance.SelectingCakeRecipeData;
        ResetRecipeElement();

        _cakeImage.sprite = cakeInfo.itemIcon;
        _cakeNameText.text = cakeInfo.itemName;

        _cakeImage.transform.parent.DOScale(1, 0.2f).SetEase(Ease.InQuart);
        _cakeImage.transform.parent.DOLocalRotateQuaternion(Quaternion.identity, 0.2f);

        _cakeNameText.transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);

        _decisionBtn.transform.DOScale(1, 0.2f).SetEase(Ease.OutBounce);
    }

    private void ResetRecipeElement()
    {
        _cakeImage.transform.parent.localScale = Vector3.one * 1.2f;
        _cakeImage.transform.parent.rotation =
        Quaternion.Euler(0, 0, Random.Range(_crookedAngleRange.x, _crookedAngleRange.y));
        _decisionBtn.transform.localScale = Vector3.zero;
    }
}
