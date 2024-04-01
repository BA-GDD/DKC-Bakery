using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform _deckElemetTrm;
    [SerializeField] private CanUseDeckElement _canUseDeckPrefab;
    [SerializeField] private SelectedDeck _selectDeckObj;

    private DeckElement _selectDeck;
    public DeckElement SelectDeck
    {
        get
        {
            return _selectDeck;
        }
        set
        {
            _selectDeck = value;
            SetSelectDeck(value.deckName, DeckManager.Instance.GetDeck(value.deck));
        }
    }

    private SaveDeckData _saveDeckData = new SaveDeckData();
    private const string _saveDeckDataKey = "SaveDeckDataKey";

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_saveDeckDataKey))
        {
            _saveDeckData = DataManager.Instance.LoadData<SaveDeckData>(_saveDeckDataKey);
        }

        for(int i = 0; i < _saveDeckData.SaveDeckList.Count; i++)
        {
            if(i % 3 == 0)
            {
                _deckElemetTrm.sizeDelta += new Vector2(0, 550);
            }

            CanUseDeckElement cude = Instantiate(_canUseDeckPrefab, _deckElemetTrm);
            cude.SetDeckInfo(_saveDeckData.SaveDeckList[i], this);
        }
    }

    public void SetSelectDeck(string deckName, List<CardBase> deck)
    {
        if(deck == null)
        {
            _selectDeckObj.gameObject.SetActive(false);
            return;
        }

        _selectDeckObj.gameObject.SetActive(true);
        _selectDeckObj.SetDeckInfo(deckName, deck);
    }
}
