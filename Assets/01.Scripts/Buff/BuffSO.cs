using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NormalBuff 
{
    public StatType type;
    public int value;
}

[CreateAssetMenu(menuName = "SO/Buff")]
public class BuffSO : ScriptableObject
{
    public List<NormalBuff> statTypes = new();
    public List<SpecialBuff> specialBuffs = new();

    public void SetOwner(Entity owner)
    {
        specialBuffs.ForEach(b => b.SetOwner(owner));
    }
}
