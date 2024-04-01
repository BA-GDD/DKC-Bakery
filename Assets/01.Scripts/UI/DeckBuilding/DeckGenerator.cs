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
            SetSelectDeck(value);
        }
    }

    private SaveDeckData _saveDeckData = new SaveDeckData();
    private const string _saveDeckDataKey = "SaveDeckDataKey";

    protected List<CanUseDeckElement> _currentDeckList = new List<CanUseDeckElement>();

    protected virtual void Start()
    {
        ResetDeckList();
    }

    public void ResetDeckList()
    {
        if (DataManager.Instance.IsHaveData(_saveDeckDataKey))
        {
            _saveDeckData = DataManager.Instance.LoadData<SaveDeckData>(_saveDeckDataKey);
        }

        foreach(Transform t in _deckElemetTrm)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < _saveDeckData.SaveDeckList.Count; i++)
        {
            if (i % 3 == 0)
            {
                _deckElemetTrm.sizeDelta += new Vector2(0, 550);
            }

            CanUseDeckElement cude = Instantiate(_canUseDeckPrefab, _deckElemetTrm);
            _currentDeckList.Add(cude);
            cude.SetDeckInfo(_saveDeckData.SaveDeckList[i], this);
        }
    }

    protected virtual void SetSelectDeck(DeckElement deckElement)
    {
        if(deckElement.deck == null)
        {
            _selectDeckObj.gameObject.SetActive(false);
            return;
        }

        _selectDeckObj.gameObject.SetActive(true);
        _selectDeckObj.SetDeckInfo(deckElement.deckName, 
                                   DeckManager.Instance.GetDeck(deckElement.deck));
    }
}
