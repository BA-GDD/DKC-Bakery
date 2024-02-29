using DG.Tweening;
using UnityEngine;

public class SlashDownController : PoolableMono
{
    private SlashDownSkill _skill;
    private Rigidbody2D _rigid;

    private Player _player;

    // 추후 이펙트 삽입

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void SetUpSlash(SlashDownSkill skill, Transform origin, Vector2 direction, Player player)
    {
        _skill = skill;
        _player = player;

        transform.position = origin.transform.position;

        //_rigid.velocity = direction;

        if (direction.x > 0)
        {
            transform.Rotate(0, -180, 0);
        }

        Sequence seq = DOTween.Sequence();
        ParticleSystem ps;
        seq.AppendInterval(_skill.slashDuration);
        //seq.Append(ps.colo());
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            DamageToTarget(enemy);
        }
    }

    private void DamageToTarget(Enemy target)
    {
        if (_skill.canFrezze)
            target.FreezeTimerFor(_skill.frezzeTime);

        Vector2 direction = (target.transform.position - transform.position).normalized;
        int damage = Mathf.RoundToInt(_player.CharStat.GetDamage() * _skill.damageMultiplier); //배율에 따라 증뎀.
        target.HealthCompo.ApplyDamage(damage, direction, _skill.knockbackPower, GameManager.Instance.Player);

        //if (_skill.canAilment)
        //{
        //    target.HealthCompo.SetAilment(Ailment.Chilled, _skill.ailmentTime, 0);
        //}

        //SkillManager.Instance.UseSkillFeedback(PlayerSkill.Sword);
    }

    public override void Init()
    {

    }
}
