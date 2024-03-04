using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    private bool _canStartFollowOwner;
    private Transform _ownerOfThisHpBar;
    public Transform OwnerOfThisHpBar
    {
        set
        {
            _ownerOfThisHpBar = value;
            _canStartFollowOwner = true;
        }
    }

    private void Update()
    {
        if (!_canStartFollowOwner) return;

        transform.position = MaestrOffice.GetScreenPosToWorldPos(_ownerOfThisHpBar.position);
    }

    public void HandleHealthChanged(float generatedHealth)
    {
        Debug.Log(generatedHealth);
    }
}
