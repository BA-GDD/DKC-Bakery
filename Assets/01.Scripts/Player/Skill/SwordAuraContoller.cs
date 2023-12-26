using DG.Tweening;
using UnityEngine;

public class SwordAuraContoller : MonoBehaviour
{
    private SwordAuraSkill _skill;
    private Rigidbody2D _rigid;
    private SpriteRenderer _render;
<<<<<<< Updated upstream
=======

    private Player _player;
>>>>>>> Stashed changes

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _render = GetComponent<SpriteRenderer>();

    }

    public void SetUpAura(SwordAuraSkill skill, Transform origin, Vector2 direction, Player player)
    {
        _skill = skill;
<<<<<<< Updated upstream
=======
        _player = player;
>>>>>>> Stashed changes

        transform.position = origin.transform.position;

        _rigid.velocity = direction;
        if (direction.x < 0)
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
<<<<<<< Updated upstream
=======
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            DamageToTarget(enemy);
        }
    }
    private void DamageToTarget(Enemy target)
    {
        if (_skill.canFrezze)
            target.FreezeTimerFor(_skill.frezzeTime);

        //데미지 옵션
        Vector2 direction = (target.transform.position - transform.position).normalized;
        int damage = Mathf.RoundToInt(_player.CharStat.GetDamage() * _skill.damageMultiplier); //배율에 따라 증뎀.
        target.HealthCompo.ApplyDamage(damage, direction, _skill.knockbackPower, GameManager.Instance.Player);

        //if (_skill.canAilment)
        //{
        //    target.HealthCompo.SetAilment(Ailment.Chilled, _skill.ailmentTime, 0);
        //}

        //데미지 줄때마다 소드 스킬 피드백 발동시키기.(소드는 UseSkill을 안써)
        //SkillManager.Instance.UseSkillFeedback(PlayerSkill.Sword);
    }
>>>>>>> Stashed changes
}
