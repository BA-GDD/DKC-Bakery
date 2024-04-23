using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildingUI : SceneUI
{
    public override void SceneUIStart()
    {
        SceneObserver.BeforeSceneType = SceneType.Lobby;
    }
}
