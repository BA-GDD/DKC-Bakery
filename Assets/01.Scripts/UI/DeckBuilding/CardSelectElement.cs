using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSelectElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _visual;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private GameObject _usingMask;
    private CardBase _cardInfo;
    public CardBase CardBase => _cardInfo;
    private DeckBuilder _deckBuilder;

    public bool IsSelect
    {
        set
        {
            _usingMask.SetActive(value);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _deckBuilder.AddDeck(CardBase);
        IsSelect = true;
    }

    public void RemoveThisCardInDeck()
    {
        _deckBuilder.RemoveInDeck(CardBase);
        IsSelect = false;
    }

    public void SetInfo(CardBase info, DeckBuilder deckBuilder)
    {
        _cardInfo = info;
        _deckBuilder = deckBuilder;

        _visual.sprite = info.CardInfo.CardVisual;
        _nameText.text = info.CardInfo.CardName; 
    }
}