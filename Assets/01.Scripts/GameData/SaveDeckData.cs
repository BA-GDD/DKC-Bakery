using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DeckElement
{
    public string deckName;
    public List<CardBase> deck;

    public DeckElement(string _deckName, List<CardBase> _deck)
    {
        deckName = _deckName;
        deck = _deck;
    }
}

public class SaveDeckData : CanSaveData
{
    public List<DeckElement> SaveDeckList = new List<DeckElement>();

    public override void SetInitialValue()
    {

    }
}
