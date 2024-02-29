using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MonsterGimic
{
    public Enemy enemy;
    public Transform appearTrm;
}

public class BattleController : MonoBehaviour
{
    private List<List<MonsterGimic>> _monsterGimicList = new ();
    private Stage _currentStage;

    private void Start()
    {
        _currentStage = FindObjectOfType<Stage>();
    }
}
