using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckBuilder : MonoBehaviour
{
    private SaveDeckData _saveDeckData = new SaveDeckData();
    private const string _saveDeckDataKey = "SaveDeckDataKey";

    [SerializeField] private GameObject _doNotSaving;
    public ExpansionList<CardBase> selectCardList = new ExpansionList<CardBase>();
    [SerializeField] private SelectCard[] _selectCardElementArr = new SelectCard[5];

    private bool _isDeckSaving;
    public bool IsDeckSaving
    {
        get
        {
            return _isDeckSaving;
        }
        set
        {
            _isDeckSaving = value;
            _doNotSaving.SetActive(!_isDeckSaving);
        }
    }

    [SerializeField] private UnityEvent<string> _errorEvent = null;

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_saveDeckDataKey))
        {
            _saveDeckData = DataManager.Instance.LoadData<SaveDeckData>(_saveDeckDataKey);
        }
    }

    public void AddDeck(CardBase cardBase)
    {
        if(selectCardList.Count >= 5)
        {
            _errorEvent?.Invoke(ErrorTextBase.deckCountError);
            return;
        }

        for(int i = 0; i <  selectCardList.Count; i++)
        {
            if (selectCardList[i].CardInfo.CardName == cardBase.CardInfo.CardName)
            {
                _errorEvent?.Invoke(ErrorTextBase.cardTypeError);
                return;
            }
        }

        selectCardList.Add(cardBase);
        _selectCardElementArr[selectCardList.Count - 1].SetCard(cardBase.CardInfo);
    }

    public void RemoveInDeck(CardBase cardBase)
    {
        int idx = selectCardList.IndexOf(cardBase);
        selectCardList.Remove(cardBase);

        _selectCardElementArr[idx].UnSetCard();
    }

    public void SaveDeck(string deckName)
    {
        if (selectCardList.Count < 5)
        {
            _errorEvent?.Invoke(ErrorTextBase.plzAllSelectError);
            return;
        }

        if (deckName.Length > 20 || deckName.Length == 0)
        {
            _errorEvent?.Invoke(ErrorTextBase.deckNameError);
            return;
        }

        IsDeckSaving = true;

        DeckElement de = new DeckElement(deckName, selectCardList);
        _saveDeckData.SaveDeckList.Add(de);
        DataManager.Instance.SaveData(_saveDeckData, _saveDeckDataKey);
    }
}
