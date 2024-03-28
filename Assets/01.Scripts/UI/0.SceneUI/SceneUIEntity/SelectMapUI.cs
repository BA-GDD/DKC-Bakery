using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapUI : SceneUI
{
    [SerializeField] private MapSelectPanelLock[] _panelLockArr;
    private readonly string _dataKey = "AdventureKEY";
    private AdventureData _adventureData = new AdventureData();

    public override void SceneUIStart()
    {
        SceneObserver.BeforeSceneType = SceneType.Lobby;

        GenerateUnLockPanel();
    }

    public AdventureData GetAdventureData()
    {
        return DataManager.Instance.LoadData<AdventureData>(_dataKey);
    }

    private void GenerateUnLockPanel()
    {
        if (DataManager.Instance.IsHaveData(_dataKey))
        {
            _adventureData = DataManager.Instance.LoadData<AdventureData>(_dataKey);
        }

        for(int i = 0; i < _adventureData.canChallingeChapterArr.Length; i++)
        {
            if (_adventureData.canChallingeChapterArr[i][0])
            {
                if (_adventureData.IsLookUnLockProductionArr[i])
                {
                    _panelLockArr[i].UnLockStageWithOutProduction();
                }
                else
                {
                    _panelLockArr[i].UnLockStageWithProduction();
                    _adventureData.IsLookUnLockProductionArr[i] = true;
                }
            }
        }

        DataManager.Instance.SaveData(_adventureData, _dataKey);
    }
}
