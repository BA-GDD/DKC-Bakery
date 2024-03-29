using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public struct CardAndEffect
{
    public CardInfo info;
    public ParticleSystem[] particle;
}

public class PlayerVFXManager : MonoBehaviour
{
    [SerializeField] private List<CardAndEffect> cardAndEffects = new();
    private Dictionary<CardInfo, ParticleSystem[]> _cardByEffects = new();
    //���ݽ� ����Ʈ ������ ����
    public Action OnEndEffectEvent;
    //public Action OnEffectEvent;

    [SerializeField] private SpriteRenderer[] backgrounds;
    private SpriteRenderer currentBackground;

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

    private void Start()
    {
        foreach (var b in backgrounds)
        {
            if (b.gameObject.activeSelf == true)
            {
                currentBackground = b;
            }
        }
    }

    internal void EndParticle(CardInfo cardInfo, int combineLevel)
    {
        if (!_cardByEffects.ContainsKey(cardInfo))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        _cardByEffects[cardInfo][combineLevel].Stop();
    }

    public void PlayParticle(CardInfo card, Vector3 pos, int combineLevel)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }

        _cardByEffects[card][combineLevel].transform.position = pos;
        _cardByEffects[card][combineLevel].gameObject.SetActive(true);
        currentBackground.DOColor(Color.gray, 1.0f);
        ParticleSystem.MainModule mainModule = _cardByEffects[card][combineLevel].main;
        StartCoroutine(EndEffectCo(mainModule.startLifetime.constantMax / mainModule.simulationSpeed));
        _cardByEffects[card][combineLevel].Play();
    }

    public void PlayParticle(CardInfo card, int combineLevel)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }

        _cardByEffects[card][combineLevel].gameObject.SetActive(true);
        currentBackground.DOColor(Color.gray, 1.0f);
        ParticleSystem.MainModule mainModule = _cardByEffects[card][combineLevel].main;
        StartCoroutine(EndEffectCo(mainModule.startLifetime.constantMax / mainModule.simulationSpeed));
        _cardByEffects[card][combineLevel].Play();
    }

    private IEnumerator EndEffectCo(float f)
    {
        yield return new WaitForSeconds(f);
        currentBackground.DOColor(Color.white, 1.0f);
        OnEndEffectEvent?.Invoke();
    }

    public void BackgroundColor(Color color)
    {
        currentBackground.DOColor(color, 1.0f);
    }
}
