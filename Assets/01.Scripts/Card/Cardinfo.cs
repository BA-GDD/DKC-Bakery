using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

[CreateAssetMenu(menuName = "SO/Card/CardInfo")]
public class CardInfo : ScriptableObject
{
    [Header("카드 정보")]
    public string CardName;
    public CardType CardType;
    public Sprite CardVisual;

    [Header("스킬 정보")]
    public string AbillityName;
    public string AbillityInfo;
    public int AbillityCost;
}
