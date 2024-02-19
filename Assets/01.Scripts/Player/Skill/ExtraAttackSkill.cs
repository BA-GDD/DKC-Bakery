using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAttackSkill : Skill
{
    [Header("추가 공격 정보")]
    [SerializeField] private ExtraAttackController _extraAttackPrefab;

    public float damageMultiplier = 2.5f;
    public Vector2 knockbackPower;

    public bool canFrezze;
    public float frezzeTime = 0.3f;

    public float extraAttackDuration = 0.5f;

    public void SpawnExtraAttack()
    {
        ExtraAttackController newExtraAttack = Instantiate(_extraAttackPrefab);

        newExtraAttack.SetUpExtraAttack(this, _player.transform, new Vector2(_player.FacingDirection, 0).normalized, _player);
    }
}