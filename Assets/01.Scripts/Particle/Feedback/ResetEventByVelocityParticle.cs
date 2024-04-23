using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particle.Trigger;

public class ResetEventByVelocityParticle : ParticleTriggerEventBase
{
    public enum velocityType
    {
        x,
        y
    }
    [SerializeField] private velocityType velType;
    private List<IUseInit> inits;
    private float velocity;

    private void Awake()
    {
        GetComponents<IUseInit>(inits);
    }
    public override void Action(ref ParticleSystem.Particle p, Collider2D col)
    {
        switch (velType)
        {
            case velocityType.x:
                if (p.velocity.x != 0 && Mathf.Sign(velocity) != Mathf.Sign(p.velocity.x))
                {
                    velocity = p.velocity.x;
                    InitEvents();
                }
                break;
            case velocityType.y:
                if (p.velocity.y != 0&&Mathf.Sign(velocity) != Mathf.Sign(p.velocity.y))
                {
                    velocity = p.velocity.y;
                    InitEvents();
                }
                break;
        }
    }
    private void InitEvents()
    {
        if (velocity == 0) return;
        foreach (var item in inits)
        {
            item.Init();
        }
    }
}
