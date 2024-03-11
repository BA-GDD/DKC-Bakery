using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] private Transform _deckCaseTrm;

    private CanUseCardData _canUseCardData =new CanUseCardData();
    private SaveDeckData _saveDeckData = new SaveDeckData();

    private const string _canUseCardDataKey = "CanUseCardsDataKey";
    private const string _saveDeckDataKey = "SaveDeckDataKey";

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_canUseCardDataKey))
        {
            _canUseCardData = DataManager.Instance.LoadData<CanUseCardData>(_canUseCardDataKey);
        }
        
        if(DataManager.Instance.IsHaveData(_saveDeckDataKey))
        {
            _saveDeckData = DataManager.Instance.LoadData<SaveDeckData>(_saveDeckDataKey);
        }
    }

    public void AddDeck(CardBase cardBase)
    {

    }

    public void RemoveInDeck(CardBase cardBase)
    {

    }

    public void SaveDeck(string deckName, List<CardBase> deck)
    {
        DeckElement de = new DeckElement(deckName, deck);
        _saveDeckData.SaveDeckList.Add(de);
        DataManager.Instance.SaveData(_saveDeckData, _saveDeckDataKey);
    }
}
