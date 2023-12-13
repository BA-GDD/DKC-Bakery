using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentWindows
{
    public List<InventoryItem> equipments;
    public Dictionary<ItemDataEquipmentSO, InventoryItem> equipmentDictionary;

    protected Transform _parentTrm;
    protected EquipmentSlot[] _equipmentSlots;

    public EquipmentWindows(Transform parnetTrm)
    {
        _parentTrm = parnetTrm;

        equipments = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipmentSO, InventoryItem>();

        _equipmentSlots = parnetTrm.GetComponentsInChildren<EquipmentSlot>();
    }

    public void UpdateSlotUI()
    {
        for (int i = 0; i < _equipmentSlots.Length; i++)
        {
            EquipmentSlot currnetSlot = _equipmentSlots[i];
            ItemDataEquipmentSO equipment = equipmentDictionary.Keys.ToList().Find(x => x.equipmentType == currnetSlot.slotTpye);

            if(equipment != null)
            {
                currnetSlot.UpdateSlot(equipmentDictionary[equipment]);
            }
            else
            {
                currnetSlot.CleanUpSlot();
            }
        }
    }
    public ItemDataEquipmentSO GetEquipmentByType(EquipmentType type)
    {
        ItemDataEquipmentSO targetItem = null;

        foreach (var pair in equipmentDictionary)
        {
            if(pair.Key.equipmentType == type)
            {
                targetItem = pair.Key;
                break;
            }
        }
        return targetItem;
    }

    public void EquipItem(ItemDataEquipmentSO equipment)
    {
        InventoryItem newItem = new InventoryItem(equipment);
        ItemDataEquipmentSO oldEquipment = GetEquipmentByType(equipment.equipmentType);

        if(oldEquipment != null)
        {
            UnEquipItem(oldEquipment);
        }
        equipments.Add(newItem);
        equipmentDictionary.Add(equipment, newItem);
        equipment.AddModifiers();
    }
    public void UnEquipItem(ItemDataEquipmentSO oldEquipment)
    {
        if(equipmentDictionary.TryGetValue(oldEquipment, out InventoryItem invenItem))
        {
            equipments.Remove(invenItem);
            equipmentDictionary.Remove(oldEquipment);

            oldEquipment.RemoveModifiers();

            Inventory.Instance.AddItem(oldEquipment);
        }
    }

}
