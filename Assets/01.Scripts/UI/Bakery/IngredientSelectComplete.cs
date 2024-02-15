using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class IngredientSelectComplete : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private GameObject _textMark;
    [SerializeField] private GameObject[] _typeTextMarkArr;

    [Header("버튼")]
    [SerializeField] private Button _myButton;
    [SerializeField] private TextMeshProUGUI _btnOkText;
    [SerializeField] private TextMeshProUGUI _btnCancleText;
    [SerializeField] private GameObject _bakingBtn;

    [Header("렉트")]
    [SerializeField] private RectTransform _boxTrm;
    [SerializeField] private Transform[] _iconBoxArr;

    private void Start()
    {
        _myButton.onClick.AddListener(SelectComplete);
    }


    public void SelectComplete()
    {
        _btnOkText.enabled = false;
        _btnCancleText.enabled = true;

        _myButton.transform.DOLocalMoveY(-300, 0.1f);

        _textMark.SetActive(false);
        for (int i = 0; i < _typeTextMarkArr.Length; i++)
            _typeTextMarkArr[i].SetActive(false);

        for (int i = 0; i < _iconBoxArr.Length; i++)
            _iconBoxArr[i].DOLocalMoveY(0, 0.2f);

        _boxTrm.DOLocalMoveY(-350, 0.2f);
        Tween t = DOTween.To(() => _boxTrm.sizeDelta, v => _boxTrm.sizeDelta = v, new Vector2(1000, 270), 0.2f);
        t.OnComplete(()=> 
        {
            _myButton.transform.DOLocalMoveY(-190, 0.1f);
            _bakingBtn.gameObject.SetActive(true);
            _bakingBtn.transform.DOLocalMoveY(-168, 0.1f);
        });

        _myButton.onClick.RemoveAllListeners();
        _myButton.onClick.AddListener(SelectRevert);
    }

    public void SelectRevert()
    {
        _btnOkText.enabled = true;
        _btnCancleText.enabled = false;

        _textMark.SetActive(true);
        for (int i = 0; i < _typeTextMarkArr.Length; i++)
            _typeTextMarkArr[i].SetActive(true);

        for (int i = 0; i < _iconBoxArr.Length; i++)
            _iconBoxArr[i].DOLocalMoveY(-35, 0.2f);

        _bakingBtn.transform.DOLocalMoveY(-300, 0.1f);
        _boxTrm.DOLocalMoveY(-290, 0.2f);
        Tween t = DOTween.To(() => _boxTrm.sizeDelta, v => _boxTrm.sizeDelta = v, new Vector2(1000, 400), 0.2f);
        t.OnComplete(() => 
        {
            _myButton.transform.DOLocalMoveY(-72, 0.1f);
            _bakingBtn.gameObject.SetActive(false);
        });

        _myButton.onClick.RemoveAllListeners();
        _myButton.onClick.AddListener(SelectComplete);
    }
}
