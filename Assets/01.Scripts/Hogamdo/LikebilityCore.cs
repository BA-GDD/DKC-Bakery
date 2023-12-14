using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LikebilityCore : MonoBehaviour
{
    [SerializeField] private LikebilityData _likebilityShameTableSO;

    [Header("수치")]
    [SerializeField] private int _maxLevel;
    private int _currentLikebilitySahme;
    private int _likebilityLevel = 1;

    [Header("이벤트")]
    [SerializeField] private UnityEvent<int> _levelUpEvent;

    [Header("Text")]
    [SerializeField] private TextMeshPro _needAndCurrent;

    public void HandleIncreaseLikebilityObserver(int addShame)
    {
        if (_likebilityLevel >= _maxLevel - 1)
            return;

        _currentLikebilitySahme += addShame;
        int needShame = _likebilityShameTableSO.NeedShameToLevelUp(_likebilityLevel);

        if (_currentLikebilitySahme >= needShame)
        {
            while (_currentLikebilitySahme > needShame)
            {
                _currentLikebilitySahme -= needShame;
                _likebilityLevel++;

                _levelUpEvent?.Invoke(_likebilityLevel);
            }
        }

        _needAndCurrent.text = $"need : {needShame}, current : {_currentLikebilitySahme}";
        if (_likebilityLevel >= _maxLevel - 1)
        {
            _needAndCurrent.text = "MaxLevel!";
        }
    }
}
