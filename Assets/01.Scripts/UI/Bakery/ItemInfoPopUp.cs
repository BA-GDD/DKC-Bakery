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

    public void Setup(Transform ieTrm, ItemDataIngredientSO ItemInfo)
    {
        transform.SetParent(ieTrm);
        transform.localPosition = Vector3.zero;
        _itemNameText.text = ItemInfo.itemName;
        _itemIcon.sprite = ItemInfo.itemIcon;
        _itemInfoText.text = ItemInfo.itemInfo;
    }

    public override void Init()
    {

    }
}
