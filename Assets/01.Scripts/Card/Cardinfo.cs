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
    public AnimationClip abilityAnimation;

    [Header("���� Ÿ�� ����Ʈ")]
    public ParticleSystem hitEffect;

    public int AbillityCost;
    [TextArea]
    public string AbillityInfo;
}
