using System;
using System.Text;
using UnityEngine;

public enum ItemType
{
    Ingredient,
    Bread
}

[CreateAssetMenu(menuName = "SO/Items/Item")]
public class ItemDataSO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public string itemInfo;
    public Sprite itemIcon;

    [Range(0, 100)]
    public float dropChance = 50f;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}