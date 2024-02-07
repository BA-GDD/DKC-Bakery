using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class BlurObject : MonoBehaviour
{
    [SerializeField] private BakeryCombinationType _combinationType;
    public BakeryCombinationType CombinationType => _combinationType;
}
