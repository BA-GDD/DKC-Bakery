using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostCheck : MonoBehaviour
{
    private int _currentMana = 10;
    [SerializeField] private TextMeshProUGUI _costText;

    [SerializeField] private Image[] _manaArr;
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
        int diffMana =  currentMoney - _currentMana;

        Debug.Log(diffMana);
        if(diffMana > 0 )
        {
            for(int i = _currentMana; i < currentMoney + 1; i++)
            {
                Material mat = new Material(_manaArr[i].material);
                _manaArr[i].material = mat;
                DOTween.To(() => -1, v => mat.SetFloat("_dissolve_amount", v), 1, 0.4f);
            }
        }
        else
        {
            Debug.Log($"{_currentMana}, {currentMoney}");
            for(int i = _currentMana; i > currentMoney; i--)
            {
                Material mat = new Material(_manaArr[i].material);
                _manaArr[i].material = mat;
                DOTween.To(() => 1, v => mat.SetFloat("_dissolve_amount", v), -1, 0.4f);
            }
        }

        _currentMana = currentMoney;
        _costText.text = currentMoney.ToString();
    }

    private void HandleCheckExMana(int currentMana)
    {
        for (int i = 0; i < _extramanaArr.Length; i++)
        {
            //_extramanaArr[i].enabled = false;
        }

        for (int i = 0; i < currentMana; i++)
        {
            //_extramanaArr[i].enabled = true;
        }
    }
}
