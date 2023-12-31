using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemAmountText;
    [SerializeField] private Sprite _emptySlotSprite;

    public InventoryItem item;

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;

        if (item != null)
        {
            _itemImage.sprite = item.itemDataSO.itemIcon;
            if (_itemAmountText != null)
            {
                if (item.stackSize > 1)
                {
                    _itemAmountText.text = item.stackSize.ToString();
                }
                else
                {
                    _itemAmountText.text = string.Empty;
                }
            }
        }
        else
        {
            CleanUpSlot();
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        _itemImage.sprite = _emptySlotSprite;
        if (_itemAmountText != null)
        {
            _itemAmountText.text = string.Empty;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null) return;

        if (Keyboard.current.ctrlKey.isPressed)
        {
            Inventory.Instance.RemoveItem(item.itemDataSO);
            return;
        }

    }
}