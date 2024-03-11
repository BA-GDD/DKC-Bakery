using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanUseCardData : CanSaveData
{
    public List<CardBase> CanUseCardsList = new List<CardBase>();

    public override void SetInitialValue()
    {
    }
}
