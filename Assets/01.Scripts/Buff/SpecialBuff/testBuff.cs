using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBuff : SpecialBuff,IOnTakeDamage
{
    public override void Active()
    {
    }

    public void TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
