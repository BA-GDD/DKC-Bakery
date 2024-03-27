using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Main,
    Mine,
    Mission
}

public enum StageBackGround
{
    Forest,
    Dungeon,
    Myosu
}

[CreateAssetMenu(menuName ="SO/StageData")]
public class StageDataSO : ScriptableObject
{
    public string stageName;
    public StageType stageType;
    public StageBackGround stageBackGround;
    public EnemyGroupSO enemyGroup;
    public TsumegoInfo clearCondition;
    public List<CardBase> missionDeck;
}
