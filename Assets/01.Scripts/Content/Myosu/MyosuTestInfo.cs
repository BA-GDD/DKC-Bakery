using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Myosu/Info")]
public class MyosuTestInfo : ScriptableObject
{
    public string MyosuName;
    public EnemyGroupSO EnemyGroup;
    public string TsumegoInfo;
    [Header("스프라이트 크기 : (800, 450)")]
    public Sprite MyosuImg;
}
