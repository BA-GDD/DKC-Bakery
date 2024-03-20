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
                Debug.LogError("�ߺ��� �־��");
            }
        }
    }
    public void PlayParticle(CardInfo card)
    {
        if (!_cardByEffects.ContainsKey(card))
        {
            Debug.LogError("����Ʈ�� �����");
            return;
        }
        _cardByEffects[card].Play();
    }
}
