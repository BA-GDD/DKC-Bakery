using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public static class CardReader
{
    private static List<CardBase> _inHandCardList = new List<CardBase>();
    private static List<CardBase> _inDeckCardList = new List<CardBase>();
    private static List<CardBase> _captureHandList = new List<CardBase>();

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

    private static SkillCardManagement _skillCardManagement;
    public static SkillCardManagement SkillCardManagement
    {
        get
        {
            if(_skillCardManagement != null) return _skillCardManagement;
            _skillCardManagement = GameObject.FindObjectOfType<SkillCardManagement>();
            return _skillCardManagement;
        }
    }

    private static SpellCardManagement _spellCardManagement;
    public static SpellCardManagement SpellCardManagement
    {
        get
        {
            if(_spellCardManagement != null) return _spellCardManagement;
            _spellCardManagement = GameObject.FindObjectOfType<SpellCardManagement>();
            return _spellCardManagement;
        }
    }

    private static InGameError _inGameError;
    public static InGameError InGameError
    {
        get
        {
            if(_inGameError != null) return _inGameError;
            _inGameError = GameObject.FindObjectOfType<InGameError>();
            return _inGameError;
        }
    }

    public static CardBase OnPointerCard { get; set; }
    public static bool OnBinding { get; set; }

    public static CardBase ShufflingCard { get; set; }

    public static void CaptureHand()
    {
        _captureHandList = _inHandCardList;
    }

    public static bool IsSameCaptureHand()
    {
        return _captureHandList == _inHandCardList;
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

    public static int GetIdx(CardBase handCard)
    {
        return _inHandCardList.IndexOf(handCard);
    }

    public static void ShuffleInHandCard(CardBase pointerCard, CardBase shufflingCard)
    {
        ShufflingCard = shufflingCard;

        int idx1 = _inHandCardList.IndexOf(pointerCard);
        int idx2 = _inHandCardList.IndexOf(shufflingCard);

        (_inHandCardList[idx1], _inHandCardList[idx2]) =
        (_inHandCardList[idx2], _inHandCardList[idx1]);
    }

    public static int GetPosOnTopDrawCard()
    {
        return 800 - ((CountOfCardInHand() -1) * 230);
    }

    public static int GetHandPos(CardBase cardBase)
    {
        int idx = _inHandCardList.IndexOf(cardBase);
        return 800 - (idx * 230);
    }

    public static void LockHandCard(bool isLock)
    {
        foreach(CardBase card in _inHandCardList)
        {
            card.CanUseThisCard = !isLock;
        }
    }
}
