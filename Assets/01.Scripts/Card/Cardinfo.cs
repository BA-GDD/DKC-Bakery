using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDefine;

public enum TargetEnemyCount
{
    I = 1,
    II,
    III,
    IV,
    V,
    ALL
}

[CreateAssetMenu(menuName = "SO/Card/CardInfo")]
public class CardInfo : ScriptableObject
{
    [Header("ī�� ����")]
    public string CardName;
    public CardType CardType;
    public Sprite CardVisual;

    [Header("��ų ����")]
    public AnimationClip abilityAnimation;
    public TargetEnemyCount targetEnemyCount;

    [Header("���� Ÿ�� ����Ʈ")]
    public ParticleSystem hitEffect;
    public ParticleSystem targetEffect;

    public int AbillityCost;
    [TextArea]
    public string AbillityInfo;
}
