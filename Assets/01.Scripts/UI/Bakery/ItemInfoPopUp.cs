using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoPopUp : PoolableMono
{
    [SerializeField] private TextMeshProUGUI _itemNameText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TextMeshProUGUI _itemInfoText;

    public void Setup(Vector2 pos, ItemDataIngredientSO ItemInfo)
    {
        transform.localPosition = pos;
        _itemInfoText.text = ItemInfo.itemName;
        _itemIcon.sprite = ItemInfo.itemIcon;
        //_itemInfoText.text = ItemInfo.in
    }

    public override void Init()
    {

    }
}
