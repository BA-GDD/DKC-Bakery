using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UIDefine;

public class ItemElement : PoolableMono
{
    [SerializeField] private Image _itemImg;
    public Sprite ItemImg
    {
        set
        {
            _itemImg.sprite = value;
        }
    }
    [SerializeField] private TextMeshProUGUI _countText;
    public string CountText
    {
        set
        {
            _countText.text = value;
        }
    }

    public override void Init()
    {

    }
}
