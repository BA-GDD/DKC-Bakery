using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : SceneUI
{
    [SerializeField]
    private BattleResultPanel _battleResultPanel;
    [SerializeField] private GameObject _battleSystem;

    public Action<bool> SystemActive { get; private set; }

    public override void SceneUIStart()
    {
        SystemActive += HandleBattleSystemActive;
    }

    public override void SceneUIEnd()
    {
        SystemActive -= HandleBattleSystemActive;
    }

    protected void HandleBattleSystemActive(bool isActive)
    {
        _battleSystem.SetActive(isActive);
    }

    public void SetClear()
    {
        _battleResultPanel.gameObject.SetActive(true);
        _battleResultPanel.SetClear();
    }
}
