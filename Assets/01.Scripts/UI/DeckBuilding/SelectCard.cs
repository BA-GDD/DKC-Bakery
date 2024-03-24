using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour
{
    [SerializeField] private Sprite _noneSelectVisual;
    [SerializeField] private GameObject _noneSelectText;
    [SerializeField] private Image _cardVisual;

    public void SetCard(CardInfo info)
    {
        _noneSelectText.SetActive(false);
        _cardVisual.sprite = info.CardVisual;
        _cardVisual.color = Color.white;
    }

    public void UnSetCard()
    {
        _noneSelectText.SetActive(true);
        _cardVisual.sprite = _noneSelectVisual;
        _cardVisual.color = new Color(1, 1, 1, 0.8f);
    }
}
