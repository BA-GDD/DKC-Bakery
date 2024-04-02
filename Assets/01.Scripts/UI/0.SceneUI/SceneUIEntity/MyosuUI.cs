using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyosuUI : SceneUI
{
    private MyosuPanel _myosuPanel;

    public override void SceneUIStart()
    {
        _myosuPanel = FindObjectOfType<MyosuPanel>();
        SceneObserver.BeforeSceneType = SceneType.Lobby;
    }

    public void SetUpPanel(bool isSetUp, MyosuTestInfo myosuInfo)
    {
        MapManager.Instanace.SelectStageData = myosuInfo.stageData;
        _myosuPanel.MyosuTestInfo = myosuInfo;
        _myosuPanel.SetUpPanel(isSetUp);
    }

    public void GoToBattleScene()
    {
        GameManager.Instance.ChangeScene(SceneType.battle);
    }
}
