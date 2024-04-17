using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particle.Trigger;
public class TakeDamageParticle : ParticleTriggerEventBase
{
    public override void Action(ref ParticleSystem.Particle p)
    {
        foreach (var d in info.Damages)
        {
            foreach (var t in info.Targets)
            {
                t.ApplyDamage(d, info.Owner);
            }
        }
    }
}