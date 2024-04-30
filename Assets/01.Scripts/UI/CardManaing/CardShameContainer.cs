using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShameContainer : MonoBehaviour
{
    [SerializeField] private List<CardInfo> _cardShameDataContainer;

    public CardShameElementSO GetCardShameData(CardInfo info)
    {
        return _cardShameDataContainer.Find(x => x == info).cardShameData;
    }
}
