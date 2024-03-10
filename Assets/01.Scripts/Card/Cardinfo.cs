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

    [Header("��ų ����")]
    public string AbillityName;
    public string AbillityInfo;
}
