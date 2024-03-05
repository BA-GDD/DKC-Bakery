using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAuraSkill : Skill
{
    [Header("Aura Á¤º¸")]
    [SerializeField] private SwordAuraContoller _auraPrefab;
    [SerializeField] private float _auraSpeed = 5f;

    public float damageMultiplier = 2.5f;
    public Vector2 knockbackPower;

    public bool canFrezze;
    public float frezzeTime = 0.3f;

    public float auraDuration = 3f;


    public void SpawnAura()
    {
        SwordAuraContoller newAura = Instantiate(_auraPrefab);
        newAura.SetUpAura(this, _player.transform, new Vector2(_player.FacingDirection, 0).normalized * _auraSpeed, _player);
    }
}
