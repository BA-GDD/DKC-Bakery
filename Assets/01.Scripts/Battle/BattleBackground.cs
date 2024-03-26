using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetBG(Sprite bgSprite)
    {
        _spriteRenderer.sprite = bgSprite;
    }
}
