using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IBakingProductionObject
{
    [SerializeField] private Vector2 _normalPos;
    [SerializeField] private Vector2 _disAppearPos;
    public float EasingTime { get; set; } = 0.3f;

    public void OnProduction()
    {
        transform.DOMove(_disAppearPos, EasingTime);
    }
    public void ExitProduction()
    {
        transform.DOKill();
        transform.DOMove(_normalPos, EasingTime);
    }
}
