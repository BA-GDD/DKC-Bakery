using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawer : MonoBehaviour
{
    [SerializeField] private Transform _cardSpawnTrm;
    [SerializeField] private Transform _cardParent;
    private Queue<CardBase> _toDrawCatalog = new Queue<CardBase>();

    private bool canDraw;
    public bool CanDraw
    {
        get { return canDraw; }
        set 
        { 
            canDraw = value;
            if(canDraw && _toDrawCatalog.Count != 0)
            {
                DrawCardLogic(_toDrawCatalog.Dequeue());
            }
        }
    }

    public void DrawCard(int count)
    {
        if(count > CardReader.CountOfCardInDeck())
        {
            Debug.LogError("카드 없음!!");
        }

        CanDraw = false;
        for (int i = 0; i < count; i++)
        {
            CardBase selectInfo = CardReader.GetRandomCardInDeck();

            _toDrawCatalog.Enqueue(selectInfo);
            Debug.Log(selectInfo);
        }

        DrawCardLogic(_toDrawCatalog.Dequeue());
    }

    private void DrawCardLogic(CardBase selectInfo)
    {
        int goToX = (800 - ((CardReader.CountOfCardInHand()) * 230));

        CardBase spawnCard = Instantiate(selectInfo, _cardParent);
        CardReader.RemoveCardInDeck(selectInfo);
        CardReader.AddCardInHand(spawnCard);
        
        spawnCard.transform.position = _cardSpawnTrm.position;
        
        spawnCard.SetUpCard(goToX);
    }
}
