using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CardTest : MonoBehaviour
{
    [SerializeField] private CardBase samplePrefab;

    private void Start()
    {
        CardReader.AddCardInDeck(samplePrefab);
        CardReader.AddCardInDeck(samplePrefab);
        CardReader.AddCardInDeck(samplePrefab);

        CardReader.CardDrawer.DrawCard(3);
    }
}
