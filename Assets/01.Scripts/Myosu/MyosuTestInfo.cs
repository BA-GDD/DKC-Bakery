using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Myosu/Info")]
public class MyosuTestInfo : ScriptableObject
{
    public string MyosuName;
    public EnemyGroupSO EnemyGroup;
    public string TsumegoInfo;
    [Header("��������Ʈ ũ�� : (800, 450)")]
    public Sprite MyosuImg;
}
