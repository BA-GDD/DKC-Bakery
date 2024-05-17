using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour, IBakingProductionObject
{
    [SerializeField] private Vector2 _normalScale;
    public float EasingTime { get; set; } = 0.3f;

    public void OnProduction()
    {
        transform.DOScale(_normalScale * 1.3f, EasingTime);
    }

    public void ExitProduction()
    {
        transform.DOKill();

        transform.DOScale(_normalScale, EasingTime);
    }
}
