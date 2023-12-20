using System.Text;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Paste,
    Ingredient
}

[CreateAssetMenu(menuName = "SO/Items/Item")]
public class ItemDataSO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string description;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }
}
