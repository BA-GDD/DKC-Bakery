using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSystem : MonoBehaviour
{
    [SerializeField] private MineInfoContainer _mineContainer;
    private const string _adventureKey = "AdventureKEY";
    MineInfo _currentMineInfo;

    public void ClearStage()
    {
        _currentMineInfo.IsClearThisStage = true;
        int cf = _currentMineInfo.Floor + 1;

        _currentMineInfo = _mineContainer.GetInfoByFloor(cf);
        MineUI mineUI = UIManager.Instance.GetSceneUI<MineUI>();

        mineUI.SetFloor(_currentMineInfo.Floor.ToString(), 
                        _currentMineInfo.StageName, 
                        _currentMineInfo.ClearGem, 
                        _currentMineInfo.IsClearThisStage);



        AdventureData ad = DataManager.Instance.LoadData<AdventureData>(_adventureKey);
        ad.ClearMineFloor = (cf - 1).ToString();
        ad.InChallingingMineName = _currentMineInfo.StageName.ToString();
        DataManager.Instance.SaveData(ad, _adventureKey);
    }
}
