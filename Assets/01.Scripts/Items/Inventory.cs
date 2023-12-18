using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    public MaterialStash materialStash;


    [Header("ParentTrms")]
    [SerializeField] private Transform _materialStashTrm;
    [Header("Feedbacks")]
    [SerializeField] private FeedbackPlayer _addFeedback;

    private void Awake()
    {
        materialStash = new MaterialStash(_materialStashTrm);
    }
    private void Start()
    {
        UpdateSlotUI();
    }
    public void UpdateSlotUI()
    {
        materialStash.UpdateSlotUI();
    }
    public void AddItem(ItemDataSO item, int count = 1)
    {
        if (materialStash.CanAddItem(item))
        {
            materialStash.AddItem(item, count);
        }
        UpdateSlotUI();
    }
    public void RemoveItem(ItemDataSO item, int count = 1)
    {
        materialStash.RemoveItem(item, count);
        UpdateSlotUI();
    }
}
