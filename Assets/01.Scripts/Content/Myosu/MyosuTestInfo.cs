using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Myosu/Info")]
public class MyosuTestInfo : ScriptableObject
{
    public StageDataSO stageData;
    [Header("��������Ʈ ���� : (1 : 1")]
    public Sprite MyosuImg;
}
