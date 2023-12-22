using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAuraContoller : MonoBehaviour
{
    private SwordAuraSkill _skill;
    private Rigidbody2D _rigid;
    private SpriteRenderer _render;
    private DamageCaster _dmgCaster;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();
    }

    public void SetUpAura(SwordAuraSkill skill,Transform origin, Vector2 direction)
    {
        _skill = skill;
        _dmgCaster.SetOwner(skill.);

        transform.position = origin.transform.position;

        _rigid.velocity = direction;
        if(direction.x < 0)
        {
            transform.Rotate(0, -180, 0);
        }

        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(_skill.auraDuration);
        seq.Append(_render.DOFade(0, 0.4f));
        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _dmgCaster.CastDamage();
    }
}
