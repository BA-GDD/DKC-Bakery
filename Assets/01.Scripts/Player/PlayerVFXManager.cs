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
    //공격시 이펙트 나오게 설정
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        foreach (var c in cardAndEffects)
        {
            if (!_cardByEffects.ContainsKey(c.info))
            {
                _cardByEffects.Add(c.info, c.particle);
            }
            else
            {
                Debug.LogError("중복이 있어요");
            }
        }
    }
    public void PlayParticle(CardInfo card)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("이펙트가 없어요");
            return;
        }
        _cardByEffects[card].Play();
    }
}
