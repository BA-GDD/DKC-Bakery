using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapUI : SceneUI
{
    public override void SceneUIStart()
    {
        SceneObserver.BeforeSceneType = SceneType.Lobby;
    }
}
