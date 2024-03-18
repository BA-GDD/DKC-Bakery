using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void ApplyDamage(int damage, Entity dealer);

    //상태이상 걸기
    public void SetAilment(Ailment ailment, int duration, int damage);
}