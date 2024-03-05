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

    public void Setup(Transform ieTrm, Transform panelParent, ItemDataIngredientSO ItemInfo)
    {
        transform.position = ieTrm.position;
        transform.SetParent(panelParent);
        transform.localScale = Vector3.one;

        _itemNameText.text = ItemInfo.itemName;
        _itemIcon.sprite = ItemInfo.itemIcon;
        _itemInfoText.text = ItemInfo.itemInfo;
    }

    public override void Init()
    {

    }
}
