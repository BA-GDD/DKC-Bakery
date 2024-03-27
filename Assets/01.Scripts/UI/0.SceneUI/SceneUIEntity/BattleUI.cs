using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : SceneUI
{
    [SerializeField]
    private BattleResultPanel _battleResultPanel;

    public void SetClear()
    {
        _battleResultPanel.gameObject.SetActive(true);
        _battleResultPanel.SetClear();
    }
}
