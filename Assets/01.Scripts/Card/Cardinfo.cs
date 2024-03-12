using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

[CreateAssetMenu(menuName = "SO/Card/CardInfo")]
public class CardInfo : ScriptableObject
{
    [Header("ī�� ����")]
    public string CardName;
    public CardType CardType;
    public Sprite CardVisual;

    [Header("��ų ����")]
    public string AbillityName;
    public string AbillityInfo;
    public int AbillityCost;
}
