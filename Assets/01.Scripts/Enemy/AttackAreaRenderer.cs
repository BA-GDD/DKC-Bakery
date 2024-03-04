using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaRenderer : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _renderers = new List<SpriteRenderer>();
    public virtual void Render()
    {
        foreach (var r in _renderers)
        {
            r.gameObject.SetActive(true);
        }
    }
    public void Off()
    {
        foreach (var r in _renderers)
        {
            r.gameObject.SetActive(false);
        }
    }
}