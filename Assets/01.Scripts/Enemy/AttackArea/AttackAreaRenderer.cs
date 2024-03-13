using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AttackAreaRenderer : MonoBehaviour
{
    protected SpriteRenderer spriteRenderers;
    private void Awake()
    {
        spriteRenderers = GetComponent<SpriteRenderer>();
    }
    public virtual void Render()
    {
        gameObject.SetActive(true);
    }
    public void Off()
    {
        gameObject.SetActive(false);
    }
}