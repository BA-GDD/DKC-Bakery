using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveDoor : MonoBehaviour, IBakingProductionObject
{
    [SerializeField] private float _normalY = 1.78f;
    [SerializeField] private float _openY = 4.5f;
    [SerializeField] private float _easingTime;

    public float EasingTime { get; set; } = 0.3f;

    public void OnProduction()
    {
        transform.DOKill();
        transform.DOMoveY(_openY, _easingTime);
    }

    public void ExitProduction()
    {
        transform.DOKill();
        transform.DOMoveY(_normalY, _easingTime);
    }
}
