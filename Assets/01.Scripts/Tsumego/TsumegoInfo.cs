using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Tsumego Info", menuName = "SO/Tsumego/Info")]
public class TsumegoInfo : ScriptableObject
{
    public bool IsClear;
    public string Name;

    public List<TsumegoCondition> Conditions;

}
