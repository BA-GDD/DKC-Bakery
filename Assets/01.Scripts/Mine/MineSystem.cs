using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSystem : MonoBehaviour
{
    [Header("¸Ê")]
    [SerializeField] private Transform _firstMap;
    [SerializeField] private Transform _secondMap;
    [SerializeField] private Vector3 _downPos;

    [SerializeField] private MineInfoContainer _mineContainer;
    private const string _adventureKey = "AdventureKEY";
    private MineInfo _currentMineInfo;
    private AdventureData _addData = new AdventureData();

    private void Start()
    {
        if(DataManager.Instance.IsHaveData(_adventureKey))
        {
            _addData = DataManager.Instance.LoadData<AdventureData>(_adventureKey);
        }

        _currentMineInfo = _mineContainer.GetInfoByFloor(Convert.ToInt16(_addData.ClearMineFloor)+1);

        MineUI mineUI = UIManager.Instance.GetSceneUI<MineUI>();
        mineUI.SetFloor(_currentMineInfo.Floor.ToString(),
                        _currentMineInfo.StageName,
                        _currentMineInfo.ClearGem,
                        _currentMineInfo.IsClearThisStage);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ClearStage();
        }
    }

    public void ClearStage()
    {
        _currentMineInfo.IsClearThisStage = true;
        int uf = _currentMineInfo.Floor + 1;
        Debug.Log(uf);

        _currentMineInfo = _mineContainer.GetInfoByFloor(uf);
        MineUI mineUI = UIManager.Instance.GetSceneUI<MineUI>();

        mineUI.SetFloor(_currentMineInfo.Floor.ToString(), 
                        _currentMineInfo.StageName, 
                        _currentMineInfo.ClearGem, 
                        _currentMineInfo.IsClearThisStage);
        MapChange();
        _addData.ClearMineFloor = (uf - 1).ToString();
        _addData.InChallingingMineName = _currentMineInfo.StageName.ToString();
        DataManager.Instance.SaveData(_addData, _adventureKey);
    }

    private void MapChange()
    {
        Transform upTrm;
        Transform downTrm;

        if(_firstMap.position.y > _secondMap.position.y)
        {
            upTrm = _firstMap;
            downTrm = _secondMap;
        }
        else
        {
            upTrm = _secondMap;
            downTrm = _firstMap;
        }
        
        upTrm.transform.position = _downPos;
        upTrm.gameObject.SetActive(true);

        _firstMap.DOMoveY(_firstMap.position.y + 14, 2f);
        _secondMap.DOMoveY(_secondMap.position.y + 14, 2f).OnComplete(()=> 
        {
            upTrm.GetComponentInChildren<MineTape>().LockTape(false);
            downTrm.GetComponentInChildren<MineTape>().LockTape(true);
            downTrm.gameObject.SetActive(false);
        });
    }
}
