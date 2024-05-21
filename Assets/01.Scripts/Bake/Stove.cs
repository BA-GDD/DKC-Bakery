using DG.Tweening;
using ExtensionFunction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour, IBakingProductionObject
{
    [SerializeField] private Vector2 _normalScale;
    public float EasingTime { get; set; } = 0.3f;

    public void OnProduction()
    {
        transform.SmartScale(_normalScale * 1.3f, EasingTime);
    }

    public void ExitProduction()
    {
        transform.SmartScale(_normalScale, EasingTime);
    }
}
