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

            _useMask.SetActive(value.isUsed);
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

    [SerializeField] private GameObject _useMask;

    public override void Init()
    {
        
    }

    public void ActiveUpdateIngredientUseMask()
    {
        _useMask.SetActive(IngredientSO.isUsed);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IngredientSO.isUsed) return;

        BakingManager.Instance.AddItem(IngredientSO);
        BakingManager.Instance.CookingBox.AddSelectIngredientInfo(IngredientSO);
        Inventory.Instance.RemoveItem(IngredientSO);

        IngredientSO.isUsed = true;
        ActiveUpdateIngredientUseMask();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _popupPanel = PoolManager.Instance.Pop(PoolingType.ItemInfoPopUpPanel) as ItemInfoPopUp;
        _popupPanel.transform.SetParent(PopUpPanelParent);
        _popupPanel.Setup(transform, PopUpPanelParent, IngredientSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PoolManager.Instance.Push(_popupPanel);
    }
}
