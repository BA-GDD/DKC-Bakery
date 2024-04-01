using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UITouchEffect : MonoBehaviour
{
    private EffectObject effectObject;
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PoolableMono pam = PoolManager.Instance.Pop(PoolingType.TouchEffect);
            pam.transform.position = MaestrOffice.GetWorldPosToScreenPos(Input.mousePosition);
        }
    }
}
