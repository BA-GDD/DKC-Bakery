using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : PoolableMono
{
    [SerializeField]
    private ParticleSystem mainParticleSystem;

    [SerializeField]
    private float pushTime = 1.0f;

    public override void Init()
    {
        mainParticleSystem.Stop();
        mainParticleSystem.Play();

        StartCoroutine(WaitForEnd());
    }

    private void PushThis()
    {
        mainParticleSystem.Stop();
        PoolManager.Instance.Push(this);
    }

    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(pushTime);
        PushThis();
    }
}