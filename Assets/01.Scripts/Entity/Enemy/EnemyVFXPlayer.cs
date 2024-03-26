using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXPlayer : MonoBehaviour
{
    public event Action OnEndEffect;

    public void PlayParticle(ParticleSystem ps, float particleDuration)
    {
        ps.gameObject.SetActive(true);
        ps.Play();
        StartCoroutine(WaitParticleEndCor(ps,particleDuration));
    }
    private IEnumerator WaitParticleEndCor(ParticleSystem ps, float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        ps.gameObject.SetActive(false);
        ps.Stop();
        OnEndEffect?.Invoke();
    }

}
