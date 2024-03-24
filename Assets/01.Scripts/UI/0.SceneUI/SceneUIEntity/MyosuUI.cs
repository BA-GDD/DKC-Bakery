using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyosuUI : SceneUI
{
    private MyosuPanel _myosuPanel;

    public override void SceneUIStart()
    {
        _myosuPanel = FindObjectOfType<MyosuPanel>();
    }

    public void SetUpPanel(bool isSetUp, MyosuTestInfo myosuInfo)
    {
        _myosuPanel.MyosuTestInfo = myosuInfo;
        _myosuPanel.SetUpPanel(isSetUp);
    }
}
