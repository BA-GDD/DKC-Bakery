using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

struct CardData
{
    public Image Visual;
    public TextMeshProUGUI NameText;

    public bool isSelect;
    public CardBase CardBase;
}

public class CardUpgradeCard : MonoBehaviour, IPointerClickHandler
{
    private CardData _cardData;
    
    private CardBase _cardBase;
    public CardBase CardBase => _cardBase;

    private RectTransform _cardRectTrm;

    private CardUpgradeUI _upgradeUI;
    
    private void Awake()
    {
        _cardRectTrm = GetComponent<RectTransform>();
        _upgradeUI = GetComponentInParent<CardUpgradeUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _cardData.isSelect = !_cardData.isSelect;

        if (_cardData.isSelect)
        {
            transform.SetParent(_upgradeUI.CanvasTrm.transform);
            _cardRectTrm.DOAnchorPos(new Vector2(_upgradeUI.CanvasTrm.sizeDelta.x, -_upgradeUI.CanvasTrm.sizeDelta.y)/2, .1f);
            _upgradeUI.CardUpgradeCard = this;
        }
        else
        {
            transform.SetParent(_upgradeUI.ContentTrm.transform);
            _cardRectTrm.DOAnchorPos(new Vector2(100, -80), .1f);
            _upgradeUI.CardUpgradeCard = null;
        }
    }

    public void SetInfo(CardBase info)
    {
        _cardBase = info;

        _cardData.Visual.sprite = _cardBase.CardInfo.CardVisual;
        _cardData.NameText.text = _cardBase.CardInfo.CardName;
    }

}
