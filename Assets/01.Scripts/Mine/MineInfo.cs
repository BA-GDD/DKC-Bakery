using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Mine/Info")]
public class MineInfo : MonoBehaviour
{
    public int Floor;
    public string StageName;
    public string ClearGem;
    public bool IsClearThisStage;
    public EnemyGroupSO _appearEnemyInfo;
}
