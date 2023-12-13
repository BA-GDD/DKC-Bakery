using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType slotTpye;

#if UNITY_EDITOR
    private void OnValidate()
    {
        gameObject.name = $"Equip Slot[{slotTpye.ToString()}]";
    }
#endif

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (null == item) return;

        if (Keyboard.current.ctrlKey.isPressed)
        {
            Inventory.Instance.equipmentWindows.UnEquipItem(item.itemDataSO as ItemDataEquipmentSO);
        }

        CleanUpSlot();
    }

}
