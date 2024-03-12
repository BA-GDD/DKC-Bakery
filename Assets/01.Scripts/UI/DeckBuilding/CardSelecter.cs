using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private CardSelectElement _cardSelectPrefab;
    [SerializeField] private DeckBuilder _deckBuilder;
    [SerializeField] private Transform _hasCardListTrm;
    [SerializeField] private CardBase CardBase;

    private CanUseCardData _canUseCardData = new CanUseCardData();
    private const string _canUseCardDataKey = "CanUseCardsDataKey";

    private void Start()
    {
        if (DataManager.Instance.IsHaveData(_canUseCardDataKey))
        {
            _canUseCardData = DataManager.Instance.LoadData<CanUseCardData>(_canUseCardDataKey);
        }
        _canUseCardData.CanUseCardsList.Add(CardBase);
        foreach(CardBase cb in _canUseCardData.CanUseCardsList)
        {
            CardSelectElement cse = Instantiate(_cardSelectPrefab, _hasCardListTrm);
            cse.SetInfo(cb, _deckBuilder);
        }
    }
}
