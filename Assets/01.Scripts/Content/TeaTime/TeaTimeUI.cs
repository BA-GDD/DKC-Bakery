using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using System.Xml.Serialization;

public class TeaTimeUI : SceneUI
{
    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private Image cardImage;
    [SerializeField]
    private TextMeshProUGUI cardName;

    public void SetCard(CardInfo cardInfo)
    {
        cardImage.sprite = cardInfo.CardVisual;
        cardName.text = cardInfo.CardName;
    }

    public void DirectorStart()
    {
        //SetCard();
        director.Play();
    }

    private void SaveCard()
    {

    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DirectorStart();
        }
    }
}
