using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Main,
    Mine,
    Mission
}

[CreateAssetMenu(menuName ="SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public string stageName;
    public StageType stageType;
    public EnemyGroupSO enemyGroup;
    public TsumegoInfo clearCondition;
    public List<CardBase> missionDeck;
}
