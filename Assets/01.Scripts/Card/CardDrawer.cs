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
    int idx;

    public void DrawCard(int count)
    {
        CanDraw = false;
        for (int i = 0; i < count; i++)
        {
            CardBase selectInfo = CardReader.GetRandomCardInDeck();

            _toDrawCatalog.Enqueue(selectInfo);
        }

        DrawCardLogic(_toDrawCatalog.Dequeue());
    }

    private void DrawCardLogic(CardBase selectInfo)
    {
        CardBase spawnCard = Instantiate(selectInfo, _cardParent);
        spawnCard.name = idx.ToString();
        idx++;

        CardReader.RemoveCardInDeck(selectInfo);
        CardReader.AddCardInHand(spawnCard);
        
        spawnCard.transform.position = _cardSpawnTrm.position;
        spawnCard.transform.localRotation = Quaternion.identity;
        spawnCard.SetUpCard(CardReader.GetPosOnTopDrawCard(), true);
    }
}
