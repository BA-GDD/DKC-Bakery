using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public enum StatType
{

    maxHealth,
    armor,
    damage,
    criticalChance,
    criticalDamage,
    receivedDmgIncreaseValue,
}
public class CharacterStat : ScriptableObject
{
    public string characterName;
    public Sprite characterVisual;
    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat receivedDmgIncreaseValue;

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;


    protected Entity _owner;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary
            = new Dictionary<StatType, FieldInfo>();

    protected void OnEnable()
    {
        Type charStatType = typeof(CharacterStat);

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            string statName = statType.ToString();
            FieldInfo statField = charStatType.GetField(statName);
            if (statField == null)
            {
                Debug.LogError($"There are no stat! error : {statName}");
            }
            else
            {
                _fieldInfoDictionary.Add(statType, statField);
            }
        }
    }

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }


    public virtual void IncreaseStatBy(int modifyValue, Stat statToModify)
    {
        statToModify.AddModifier(modifyValue);
    }
    public virtual void DecreaseStatBy(int modifyValue, Stat statToModify)
    {
        statToModify.RemoveModifier(modifyValue);
    }

    public int GetDamage()
    {
        return damage.GetValue();
    }

    public int ArmoredDamage(int incomingDamage, bool isChilled)
    {
        int curArmor = armor.GetValue();
        if (isChilled) curArmor = curArmor >> 1;
        return Mathf.Max(incomingDamage - curArmor, 0);
    }

    public bool IsCritical(ref int incomingDamage)
    {
        if (UnityEngine.Random.value * 100 <= criticalChance.GetValue())
        {
            incomingDamage = CalculateCriticalDamage(incomingDamage);
            return true;
        }
        return false;
    }

    protected int CalculateCriticalDamage(int incomingDamage)
    {
        return incomingDamage + Mathf.RoundToInt(incomingDamage * criticalDamage.GetValue() * 0.01f);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }


    public Stat GetStatByType(StatType statType)
    {
        return _fieldInfoDictionary[statType].GetValue(this) as Stat;
    }
}
