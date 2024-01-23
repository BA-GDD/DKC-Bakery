using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundData : CanSaveData
{
    public float MasterVoume { get; set; }
    public float BgmVolume { get; set; }
    public float SfxVolume { get; set; }

    public override void SetInitialValue()
    {
        MasterVoume = 100;
        BgmVolume = 50;
        SfxVolume = 50;
    }
}
