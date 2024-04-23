using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CakeData
{
    public string CakeName;
    public bool IsFavorites;
}

public class BakeryData : CanSaveData
{
    public List<CakeData> CakeDataList = new List<CakeData>();

    public override void SetInitialValue()
    {

    }
}
