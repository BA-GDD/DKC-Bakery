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

    public Transform PopUpPanelParent { get; set; }
    private ItemInfoPopUp _popupPanel;

    public override void Init()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _popupPanel = PoolManager.Instance.Pop(PoolingType.ItemInfoPopUpPanel) as ItemInfoPopUp;
        _popupPanel.transform.SetParent(PopUpPanelParent);
        _popupPanel.Setup(transform.localPosition, IngredientSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PoolManager.Instance.Push(_popupPanel);
    }
}
