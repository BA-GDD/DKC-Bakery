using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSettingController : MonoBehaviour
{
    [SerializeField] private CardInfo _testInfo;

    private CardSetter[] _cardSetterArr;
    private CardShameContainer _cardShameContaner;

    [SerializeField] private TextMeshProUGUI _combineText;
    private int _combineLevel = 1;

    public void AddCombineLevel()
    {
        if (_combineLevel == 3) return;

        _combineLevel++;
        CardSetting(_combineLevel, _testInfo);
        SetCombineText();
    }

    public void MinusCombineLevel()
    {
        if (_combineLevel == 1) return;

        _combineLevel--;
        CardSetting(_combineLevel, _testInfo);
        SetCombineText();
    }

    private void Awake()
    {
        _cardSetterArr = GetComponentsInChildren<CardSetter>();
        _cardShameContaner = GetComponent<CardShameContainer>();

        SetCombineText();
    }

    private void SetCombineText()
    {
        _combineText.text = $"ÄÞ¹ÙÀÎ ·¹º§ : {_combineLevel}";
    }

    private void Start()
    {
        UIManager.Instance.GetSceneUI<CardManagingUI>().
        CurrentCardShameElementInfo = _testInfo.cardShameData;

        ResetInfo();
    }

    public void ResetInfo()
    {
        CardSetting(_combineLevel, _testInfo);
    }

    public void CardSetting(int combineLevel, CardInfo cardInfo)
    {
        CardShameElementSO cardShameData = _cardShameContaner.GetCardShameData(cardInfo);

        foreach(CardSetter cardSetter in _cardSetterArr)
        {
            cardSetter.SetCardInfo(cardShameData, cardInfo, combineLevel);
        }
    }
}
