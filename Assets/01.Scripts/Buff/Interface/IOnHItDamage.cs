using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnHItDamage
{
    public void HitDamage(Entity dealer, ref int damage);
}
