using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTest : MonoBehaviour
{
    [SerializeField] private CardBase samplePrefab;

    private void Start()
    {
        CardReader.AddCardInDeck(samplePrefab);
        CardReader.AddCardInDeck(samplePrefab);
        CardReader.AddCardInDeck(samplePrefab);
        CardReader.AddCardInDeck(samplePrefab);

        TurnCounter.RoundStartEvent += () => CardReader.CardDrawer.DrawCard(4);
        TurnCounter.RoundStartEvent?.Invoke();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CardReader.AddCardInDeck(samplePrefab);
            CardReader.CardDrawer.DrawCard(1);
        }
    }
}
