using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class LikebilityCore : MonoBehaviour
{
    [SerializeField] private LikebilityData _likebilityShameTableSO;

    [Header("수치")]
    private int _currentLikebilitySahme;
    private int _likebilityLevel = 1;

    [Header("이벤트")]
    [SerializeField] private UnityEvent<int> _levelUpEvent;

    public void HandleIncreaseLikebilityObserver(int addShame)
    {
        _currentLikebilitySahme += addShame;
        if(CanLevelUpLikeLevel())
        {
            _likebilityLevel++;
            _levelUpEvent?.Invoke(_likebilityLevel);
        }
    }

    private bool CanLevelUpLikeLevel()
    {
        if (_currentLikebilitySahme >= 
            _likebilityShameTableSO.NeedShameToLevelUp(_likebilityLevel))
        {
            return true;
        }
        return false;
    }
}
