using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineUI : SceneUI
{
    [SerializeField] private Animator _animator;
    private readonly int _setUpHash = Animator.StringToHash("isSetUp");

    [SerializeField] private TextMeshProUGUI _stageFloor;
    [SerializeField] private TextMeshProUGUI _stageName;
    [SerializeField] private TextMeshProUGUI _clearGemCount;
    [SerializeField] private GameObject _onlyFirstGemObj;

    private string _currentFloor;
    private string _currentStageName;
    private string _currentClearGem;
    private bool _isClearCurrentStage;

    public void SetFloor(string floor, string stageName, string clearGem, bool isClear)
    {
        _currentFloor = floor;
        _currentStageName = stageName;
        _currentClearGem = clearGem;
        _isClearCurrentStage = isClear;

        _animator.SetBool(_setUpHash, true);
    }

    public void SetUpFloor()
    {
        _stageFloor.text = _currentFloor;
        _stageName.text = _currentStageName;
        _clearGemCount.text = _currentClearGem;
        _onlyFirstGemObj.SetActive(!_isClearCurrentStage);
    }
}