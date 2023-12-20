using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloneSkillContoller : MonoBehaviour
{
    [SerializeField] private int _attackCategoryCount = 3;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
    private int _facingDIrection = 1;

    private CloneSkill _skill;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    public void SetUpClone(CloneSkill skill, Transform origin, Vector3 offset)
    {
        _animator.SetInteger(_comboCounterHash, Random.Range(0, _attackCategoryCount));
        _skill = skill;
        transform.position = origin.position + offset;

        FacingClosestTarget();

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(_skill.cloneDuration);
        seq.Append(_spriteRenderer.DOFade(0, 0.4f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
        //_skill.cloneDuration;

    }
    private void FacingClosestTarget()
    {
        Transform target = _skill.FindClosestEnemy(transform, _skill.findEnemyRadius);
        if (target != null)
        {
            if (transform.position.x > target.position.x)
            {
                _facingDIrection = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    private void AnimationEndTrigger()
    {
    }

    private void AnimationEventTrigger()
    {
    }
}
