using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void Goback()
    {
        GameManager.Instance.ChangeScene(GameManager.Instance.beforeSceneName);
    }
}
