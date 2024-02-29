using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleChangeButton : MonoBehaviour
{
    public void GoToLobby()
    {
        GameManager.Instance.ChangeScene("LobbyScene");
    }
}
