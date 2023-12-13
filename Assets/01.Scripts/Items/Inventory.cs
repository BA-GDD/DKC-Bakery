using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    public MaterialStash materialStash;
    public EquipmentStash equipmentStash;
    public EquipmentWindows equipmentWindows;


    [Header("ParentTrms")]
    [SerializeField] private Transform _materialStashTrm;
    [SerializeField] private Transform _equipmentStashTrm;
    [SerializeField] private Transform _equipmentsTrm;

    private void Awake()
    {
        materialStash = new MaterialStash(_materialStashTrm);
        equipmentStash = new EquipmentStash(_equipmentStashTrm);
        equipmentWindows = new EquipmentWindows(_equipmentsTrm);
    }
    private void Start()
    {
        UpdateSlotUI();
    }
    public void UpdateSlotUI()
    {
        materialStash.UpdateSlotUI();
        equipmentStash.UpdateSlotUI();
        equipmentWindows.UpdateSlotUI();
    }
    public void AddItem(ItemDataSO item, int count = 1)
    {
        if (item.itemType == ItemType.Equipment)
        {
            if (equipmentStash.CanAddItem(item))
            {
                equipmentStash.AddItem(item);
            }
        }
        else if (item.itemType == ItemType.Material)
        {
            if (materialStash.CanAddItem(item))
            {
                materialStash.AddItem(item, count);
            }
        }
        UpdateSlotUI();
    }
    public void RemoveItem(ItemDataSO item, int count = 1)
    {
        if (item.itemType == ItemType.Equipment)
        {
            equipmentStash.RemoveItem(item, count);
        }
        else if (item.itemType == ItemType.Material)
        {
            materialStash.RemoveItem(item, count);
        }
        UpdateSlotUI();
    }
}
