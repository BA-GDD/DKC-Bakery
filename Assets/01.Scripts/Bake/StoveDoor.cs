using DG.Tweening;
using ExtensionFunction;
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
        transform.SmartMoveY(false, _openY, _easingTime);
    }

    public void ExitProduction()
    {
        transform.SmartMoveY(false, _normalY, _easingTime);
    }
}
