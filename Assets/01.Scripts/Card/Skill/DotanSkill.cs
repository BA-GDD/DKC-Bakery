using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotanSkill : CardBase
{
    public override void Abillity()
    {
        IsActivingAbillity = true;
        player.UseAbility(this);
        player.OnAnimationCall += () => player.VFXManager.PlayParticle(CardInfo);
        player.OnAnimationEnd += () => IsActivingAbillity = false;
    }
}
