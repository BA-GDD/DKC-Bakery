using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDownSkill : Skill
{
    [Header("Slash Á¤º¸")]
    [SerializeField] private SlashDownController _slashPrefab;

    public float damageMultiplier = 2.5f;
    public Vector2 knockbackPower;

    public bool canFrezze;
    public float frezzeTime = 0.3f;

    public float slashDuration = 1f;


    public void SpawnSlash()
    {
        SlashDownController newSlash = Instantiate(_slashPrefab);
        newSlash.SetUpSlash(this, _player.transform, new Vector2(_player.FacingDirection, 0).normalized, _player);
    }
}
