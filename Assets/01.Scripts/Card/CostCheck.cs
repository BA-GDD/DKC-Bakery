using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _costText;

    private void Start()
    {
        CostCalculator.MoneyChangeEvent += HandleCheckCost;
        HandleCheckCost(CostCalculator.CurrentMoney);
    }

    private void OnDisable()
    {
        CostCalculator.MoneyChangeEvent -= HandleCheckCost;
    }

    private void HandleCheckCost(int currentMoney)
    {
        _costText.text = currentMoney.ToString();
    }
}
