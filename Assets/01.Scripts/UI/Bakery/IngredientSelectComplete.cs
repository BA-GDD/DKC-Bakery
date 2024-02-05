using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class IngredientSelectComplete : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private GameObject _textMark;
    [SerializeField] private GameObject[] _typeTextMarkArr;
    [SerializeField] private TextMesh _btnText;

    [Header("렉트")]
    [SerializeField] private RectTransform _boxTrm;
    [SerializeField] private Transform[] _iconBoxArr;
    

    public void SlelectComplete()
    {
        _btnText.text = "OK!";

        _textMark.SetActive(false);
        for (int i = 0; i < _typeTextMarkArr.Length; i++)
            _typeTextMarkArr[i].SetActive(false);

        for (int i = 0; i < _iconBoxArr.Length; i++)
            _iconBoxArr[i].DOLocalMoveY(0, 0.2f);

        _boxTrm.DOLocalMoveY(-350, 0.2f);
        Tween t = DOTween.To(() => _boxTrm.sizeDelta, v => _boxTrm.sizeDelta = v, new Vector2(1000, 270), 0.2f);

        gameObject.SetActive(false);
    }

    public void SelectRevert()
    {

    }
}
