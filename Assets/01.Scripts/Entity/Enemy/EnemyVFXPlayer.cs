using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXPlayer : MonoBehaviour
{
    public event Action OnEndEffect;

    public void PlayParticle(EnemyAttack enemyAttack, float particleDuration)
    {
        enemyAttack.attack.gameObject.SetActive(true);
        enemyAttack.attack.Play();
        StartCoroutine(WaitParticleEndCor(enemyAttack.attack, particleDuration));
    }
    public void PlayHitEffect(EnemyAttack enemyAttack, Vector3 pos)
    {
        Destroy(Instantiate(enemyAttack.hitPrefab,pos,Quaternion.identity),1.0f);
    }
    private IEnumerator WaitParticleEndCor(ParticleSystem ps, float particleDuration)
    {
        yield return new WaitForSeconds(particleDuration);
        ps.gameObject.SetActive(false);
        ps.Stop();
        OnEndEffect?.Invoke();
    }

}
