using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class CardReader
{
    private static List<CardBase> _inHandCardList = new List<CardBase>();
    private static List<CardBase> _inDeckCardList = new List<CardBase>();

    private static CardDrawer _cardDrawer;
    public static CardDrawer CardDrawer
    {
        get
        {
            if(_cardDrawer != null) return _cardDrawer;

            _cardDrawer = GameObject.FindObjectOfType<CardDrawer>();
            return _cardDrawer;
        }
    }

    private static CombineMaster _combineMaster;
    public static CombineMaster CombineMaster
    {
        get
        {
            if (_combineMaster != null) return _combineMaster;

            _combineMaster = GameObject.FindObjectOfType<CombineMaster>();
            return _combineMaster;
        }
    }

    public static void AddCardInHand(CardBase addingCardInfo)
    {
        _inHandCardList.Add(addingCardInfo);
    }

    public static void RemoveCardInHand(CardBase removingCardInfo)
    {
        _inHandCardList.Remove(removingCardInfo);
    }

    public static int CountOfCardInHand()
    {
        return _inHandCardList.Count;
    }

    public static CardBase GetCardinfoInHand(int index)
    {
        if(index < 0 || index > CountOfCardInHand()) return null;

        return _inHandCardList[index];
    }

    public static void AddCardInDeck(CardBase addingCardInfo)
    {
        _inDeckCardList.Add(addingCardInfo);
    }

    public static void RemoveCardInDeck(CardBase removingCardInfo)
    {
        _inDeckCardList.Remove(removingCardInfo);
    }

    public static int CountOfCardInDeck()
    {
        return _inDeckCardList.Count;
    }

    public static CardBase GetRandomCardInDeck()
    {
        return _inDeckCardList[Random.Range(0, _inDeckCardList.Count)];
    }
}
