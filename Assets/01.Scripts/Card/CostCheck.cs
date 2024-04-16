using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Image[] _extramanaArr;

    private void Start()
    {
        CostCalculator.MoneyChangeEvent += HandleCheckCost;
        HandleCheckCost(CostCalculator.CurrentMoney);

        CostCalculator.ExtraManaChangeEvent += HandleCheckExMana;
        HandleCheckExMana(CostCalculator.CurrentExMana);

        TurnCounter.PlayerTurnStartEvent += HandleCalculateExMana;
    }

    private void OnDisable()
    {
        CostCalculator.MoneyChangeEvent -= HandleCheckCost;
        CostCalculator.ExtraManaChangeEvent -= HandleCheckExMana;
    }

    private void HandleCalculateExMana(bool a)
    {
        CostCalculator.GetExMana(CostCalculator.CurrentMoney);
        CostCalculator.CurrentMoney = 10;
        CostCalculator.GetCost(0);
    }

    private void HandleCheckCost(int currentMoney)
    {
        _costText.text = currentMoney.ToString();
    }

    private void HandleCheckExMana(int currentMana)
    {
        for (int i = 0; i < _extramanaArr.Length; i++)
        {
            _extramanaArr[i].enabled = false;
        }

        for (int i = 0; i < currentMana; i++)
        {
            _extramanaArr[i].enabled = true;
        }
    }
}
