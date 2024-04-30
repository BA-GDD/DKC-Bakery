using CardDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardShameType
{
    Damage,
    Buff,
    Debuff,
    Cost,
    Range,
    Time
}

[Serializable]
public struct CardShameData
{
    public CardShameType cardShameType;
    public int currentShame;
    public int afterShame;
    public string info;
}

[CreateAssetMenu(menuName = "SO/Card/Data")]
public class CardShameElementSO : ScriptableObject
{
    public int cardLevel = 1;
    public float cardExp;
    public List<SEList<CardShameData>> cardShameDataList = new ();
}
