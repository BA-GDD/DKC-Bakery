using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask

}

[System.Serializable]
public struct AddStat
{
    public StatType statType;
    public int statValue;
}

[CreateAssetMenu(menuName = "SO/Items/Equipment")]
public class ItemDataEquipmentSO : ItemDataSO
{
    public EquipmentType equipmentType;
    public List<AddStat> addStats;
    
    public void AddModifiers()
    {
        PlayerStat playerStat = GameManager.Instance.Player.PlayerStat;
        foreach (AddStat addStat in addStats)
        {
            Stat stat = playerStat.GetStatByType(addStat.statType);
            stat.AddModifier(addStat.statValue);
        }
    }

    public void RemoveModifiers()
    {
        PlayerStat playerStat = GameManager.Instance.Player.PlayerStat;
        foreach (AddStat addStat in addStats)
        {
            Stat stat = playerStat.GetStatByType(addStat.statType);
            stat.RemoveModifier(addStat.statValue);
        }
    }
}
