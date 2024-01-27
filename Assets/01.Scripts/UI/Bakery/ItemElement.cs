using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemElement : PoolableMono, IPointerClickHandler
{
    [SerializeField] private Image _itemImg;
    private ItemDataIngredientSO _myIngredientSO;
    public ItemDataIngredientSO IngredientSO
    {
        get
        {
            return _myIngredientSO;
        }
        set
        {
            _myIngredientSO = value;
            _itemImg.sprite = value.itemIcon;
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

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
