using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public enum StatType
{
    strength,
    agility,
    intelligence,
    vitality,
    maxHealth,
    armor,
    evasion,
    magicResistance,
    damage,
    criticalChance,
    criticalDamage,
    fireDamage,
    ignitePercent,
    iceDamage,
    chillPercent,
    lightingDamage,
    shockPercent
}
public class CharacterStat : ScriptableObject
{
    public string characterName;
    public Sprite characterVisual;
    [Header("Major stat")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;


    [Header("Defensive stats")]
    public Stat maxHealth; //체력
    public Stat armor; //방어도
    public Stat evasion; //회피도
    public Stat magicResistance; //마법방어

    [Header("Offensive stats")]
    public Stat damage;
    public Stat criticalChance;
    public Stat criticalDamage;


    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat ignitePercent;
    public Stat iceDamage;
    public Stat chillPercent;
    public Stat lightingDamage;
    public Stat shockPercent;


    protected Entity _owner;

    protected Dictionary<StatType, FieldInfo> _fieldInfoDictionary
            = new Dictionary<StatType, FieldInfo>();

    protected void OnEnable()
    {
        Type playerStatType = typeof(PlayerStat);

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            string statName = statType.ToString();
            FieldInfo statField = playerStatType.GetField(statName);
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
        return strength.GetValue();
    }

    public bool CanEvasion()
    {
        return false;
    }

    public int ArmoredDamage(int incomingDamage, bool isChilled)
    {
        int curArmor = armor.GetValue();
        if (isChilled) curArmor = curArmor >> 1;
        return Mathf.Max(incomingDamage - curArmor, 0);
    }

    public bool IsCritical(ref int incomingDamage)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        if (UnityEngine.Random.value * 100 <= criticalChance.GetValue())
        {
            incomingDamage = CalculateCriticalDamage(incomingDamage);
            return true;
        }
=======
>>>>>>> parent of c088f6a3 (asdf)
=======
>>>>>>> parent of c088f6a3 (asdf)
        return false;
    }

    protected int CalculateCriticalDamage(int incomingDamage)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        return incomingDamage + Mathf.RoundToInt(incomingDamage * criticalDamage.GetValue() * 0.01f);
=======
        return 0;
>>>>>>> parent of c088f6a3 (asdf)
=======
        return 0;
>>>>>>> parent of c088f6a3 (asdf)
    }

    public virtual int GetMagicDamage()
    {
        return 0;
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }


    public virtual int GetMagicDamageAfterRegist(int incomingDamage)
    {
        return 0;
    }

    public Stat GetStatByType(StatType statType)
    {
        return _fieldInfoDictionary[statType].GetValue(this) as Stat;
    }
}
