using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestElement : PoolableMono, 
                           IPointerEnterHandler,
                           IPointerExitHandler,
                           IPointerClickHandler
{
    [Header("ÂüÁ¶ °ª")]
    [SerializeField] private Image _frameImg;
    [SerializeField] private TextMeshProUGUI _infoText;
    public MyosuTestInfo TestInfo { get; set; }

    public int TestIdx { get; set; }

    private bool _isSelected;
    private bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            UIManager.Instance.GetSceneUI<MyosuUI>().SetUpPanel(value, TestInfo);
        }
    }

    public override void Init()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(1);
        if (IsSelected) return;
        _frameImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelected) return;
        _frameImg.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelected = !IsSelected;
    }
}
