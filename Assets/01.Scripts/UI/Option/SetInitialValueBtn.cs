using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInitialValueBtn : MonoBehaviour
{
    public void InitializeData(CanSaveData toSaveData, out bool isHasChange)
    {
        isHasChange = false;
        toSaveData.SetInitialValue();
    }
}
