using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBtn : MonoBehaviour
{
    public void SaveData(CanSaveData toSaveData, out bool isHasChange)
    {
        isHasChange = false;
        DataManager.Instance.SaveData(toSaveData);
    }
}
