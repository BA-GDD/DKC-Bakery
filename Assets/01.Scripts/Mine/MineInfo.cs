using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Mine/Info")]
public class MineInfo : ScriptableObject
{
    public int Floor;
    public string StageName;
    public string ClearGem;
    public bool IsClearThisStage;
    public EnemyGroupSO _appearEnemyInfo;
}