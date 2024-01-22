using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    private InfoBlockSelectBtn[] _infoBlockArr;

    private void Awake()
    {
        _infoBlockArr = FindObjectsOfType<InfoBlockSelectBtn>();
    }

    public void ManagingActivationInfoBlock(InfoBlockSelectBtn infoSelectBtn)
    {
        foreach(InfoBlockSelectBtn ibsb in _infoBlockArr)
        {
            if(infoSelectBtn == ibsb)
            {
                if (infoSelectBtn.ClickThisPanel) break;

                infoSelectBtn.ClickThisPanel = true;
                continue;
            }
            ibsb.ClickThisPanel = false;
        }
    }
}
