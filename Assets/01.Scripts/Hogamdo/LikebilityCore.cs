using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LikebilityCore : MonoBehaviour
{
    [SerializeField] private LikebilityData _likebilityShameTableSO;

    [Header("수치")]
    private int _currentLikebilitySahme;
    private int _likebilityLevel;

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
        return true;
    }
}
