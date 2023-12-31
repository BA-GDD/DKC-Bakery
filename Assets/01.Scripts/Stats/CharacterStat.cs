using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


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
    [Header("Major stat")]
    public Stat strength; // 1포인트당 데미지 증가, 크증뎀 1%
    public Stat agility; // 1포인트당 회피 1%, 크리티컬 1%
    public Stat intelligence; // 1포인트당 마법데미지 1증가, 마법저항 3증가, 도트 데미지에 지능의 10% 증뎀(지능10당 도트뎀 10퍼 증가)
    public Stat vitality; // 1포인트당 체력 5증가.


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

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }


    public virtual void IncreaseStatBy(int modifyValue, float duration, Stat statToModify)
    {
        _owner.StartCoroutine(StatModifyCoroutine(modifyValue, duration, statToModify));
    }

    protected IEnumerator StatModifyCoroutine(int modifyValue, float duration, Stat statToModify)
    {
        statToModify.AddModifier(modifyValue);
        yield return new WaitForSeconds(duration);
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
        return false;
    }

    protected int CalculateCriticalDamage(int incomingDamage)
    {
        return 0;
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

}
