using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelectBtn : MonoBehaviour
{
    [SerializeField] private SelectedDeck _deck;

    public void DeckSelect()
    {
        MapManager.Instanace.SelectDeck = _deck.SelectDeck;
    }
}
