using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleChangeButton : TitleButton
{
    public override void PressEvent()
    {
        GameManager.Instance.ChangeScene("LobbyScene");
    }
}
