using DG.Tweening;
using UnityEngine;

public class SwordAuraContoller : PoolableMono
{
    private SwordAuraSkill _skill;
    private Rigidbody2D _rigid;

    private Player _player;

    [SerializeField]private ParticleSystem _auraGlow;
    [SerializeField]private ParticleSystem _auraSword;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void SetUpAura(SwordAuraSkill skill, Transform origin, Vector2 direction, Player player)
    {
        _skill = skill;
        _player = player;

        transform.position = origin.transform.position;

        _rigid.velocity = direction;

        ParticleSystem.MainModule growModule = _auraGlow.main;
        ParticleSystem.MainModule swordModule = _auraSword.main;

        ParticleSystem.MinMaxCurve startLifeTime = new ParticleSystem.MinMaxCurve(_skill.auraDuration);
        growModule.startLifetime = startLifeTime;
        swordModule.startLifetime = startLifeTime;

        if (direction.x > 0)
        {
            transform.Rotate(0, -180, 0);
        }
        _auraGlow.Play();
        _auraSword.Play();

        Sequence seq = DOTween.Sequence();
        ParticleSystem ps;
        seq.AppendInterval(_skill.auraDuration);
        //seq.Append(ps.colo());
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }
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

    public override void Init()
    {
    }
}
