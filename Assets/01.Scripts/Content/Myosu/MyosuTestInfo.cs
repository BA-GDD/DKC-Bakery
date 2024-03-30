using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Myosu/Info")]
public class MyosuTestInfo : ScriptableObject
{
    public StageDataSO stageData;
    [Header("스프라이트 비율 : (1 : 1")]
    public Sprite MyosuImg;
}
