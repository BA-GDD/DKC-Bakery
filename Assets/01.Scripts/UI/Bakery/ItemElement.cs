using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemElement : PoolableMono, 
                           IPointerClickHandler, 
                           IPointerEnterHandler, 
                           IPointerExitHandler
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

    private ItemInfoPopUp _popupPanel;
    private readonly float _popupSetUpWaitSecond = 1f;

    public override void Init()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
