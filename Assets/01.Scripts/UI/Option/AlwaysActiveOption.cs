using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class AlwaysActiveOption : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OptionPanel optionPanel =
            PanelManager.Instance.CreatePanel(PanelType.option, UIManager.Instance.CanvasTrm, Vector3.zero)
            as OptionPanel;

            optionPanel.PanelSetUp();
        }
    }
}
