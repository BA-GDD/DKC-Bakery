using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    public SaveDeckData saveDeckData = new SaveDeckData();
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

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_saveDeckDataKey))
        {
            saveDeckData = DataManager.Instance.LoadData<SaveDeckData>(_saveDeckDataKey);
        }
    }

    public void AddDeck(CardBase cardBase)
    {
        if(selectCardList.Count >= 5)
        {
            return;
        }

        for(int i = 0; i <  selectCardList.Count; i++)
        {
            if (selectCardList[i].CardInfo.CardName == cardBase.CardInfo.CardName)
            {
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
            return;
        }

        if (deckName.Length > 20)
        {
            return;
        }

        IsDeckSaving = true;

        DeckElement de = new DeckElement(deckName, selectCardList);
        saveDeckData.SaveDeckList.Add(de);
        DataManager.Instance.SaveData(saveDeckData, _saveDeckDataKey);
    }
}
