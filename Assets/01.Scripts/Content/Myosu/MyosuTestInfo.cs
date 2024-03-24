using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Myosu/Info")]
public class MyosuTestInfo : ScriptableObject
{
    public StageDataSO stageData;
    public string TsumegoInfo;
    [Header("��������Ʈ ũ�� : (800, 450)")]
    public Sprite MyosuImg;
}
