using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private CardSelectElement _cardSelectPrefab;
    [SerializeField] private DeckBuilder _deckBuilder;
    [SerializeField] private RectTransform _hasCardListTrm;
    [SerializeField] private List<CardBase> sample;

    private CanUseCardData _canUseCardData = new CanUseCardData();
    private const string _canUseCardDataKey = "CanUseCardsDataKey";

    private void Start()
    {
        if (DataManager.Instance.IsHaveData(_canUseCardDataKey))
        {
            _canUseCardData = DataManager.Instance.LoadData<CanUseCardData>(_canUseCardDataKey);
        }

        foreach(CardBase c in sample)
            _canUseCardData.CanUseCardsList.Add(c.CardInfo.CardName);

        for(int i = 0; i < _canUseCardData.CanUseCardsList.Count; i++)
        {
            if(i % 6 == 0)
            {
                _hasCardListTrm.sizeDelta += new Vector2(0, 460);
            }

            CardSelectElement cse = Instantiate(_cardSelectPrefab, _hasCardListTrm);
            cse.SetInfo(DeckManager.Instance.GetCard(_canUseCardData.CanUseCardsList[i]), _deckBuilder);
        }
    }
}
