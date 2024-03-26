using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBattleProduction : BattleProduction
{
    [SerializeField] private GameObject _turnSystem;
    private void Start()
    {
        
    }

    public void StartBattle()
    {
        _turnSystem.SetActive(true);
        StartCoroutine(ProductionCo());
    }
}
