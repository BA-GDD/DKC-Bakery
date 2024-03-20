using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CardAndEffect
{
    public CardInfo info;
    public ParticleSystem particle;
}

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private List<CardAndEffect> cardAndEffects = new();
    private Dictionary<CardInfo, ParticleSystem> _cardByEffects = new();
    //���ݽ� ����Ʈ ������ ����
    public Action OnEndEffectEvent;
    //public Action OnEffectEvent;

    private void Awake()
    {
        foreach (var c in cardAndEffects)
        {
            if (!_cardByEffects.ContainsKey(c.info))
            {
                _cardByEffects.Add(c.info, c.particle);
            }
            else
            {
                Debug.LogError("�ߺ��� �־��");
            }
        }
    }

    internal void EndParticle(CardInfo cardInfo)
    {
        if (!_cardByEffects.ContainsKey(cardInfo))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        _cardByEffects[cardInfo].Stop();
    }

    public void PlayParticle(CardInfo card)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        ParticleSystem.MainModule mainModule = _cardByEffects[card].main;
        StartCoroutine(EndEffectCo(mainModule.startLifetime.constantMax / mainModule.simulationSpeed));
        _cardByEffects[card].Play();
    }
    private IEnumerator EndEffectCo(float f)
    {
        yield return new WaitForSeconds(f);
        OnEndEffectEvent?.Invoke();
    }

}
