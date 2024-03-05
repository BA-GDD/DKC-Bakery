using DG.Tweening;
using UnityEngine;

public class ExtraAttackController : PoolableMono
{
    private ExtraAttackSkill _skill;

    private Player _player;

    public void SetUpExtraAttack(ExtraAttackSkill skill, Transform origin, Vector2 direction, Player player)
    {
        _skill = skill;
        _player = player;

        transform.position = origin.transform.position;

        if (direction.x > 0)
        {
            transform.Rotate(0, -180, 0);
        }

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(_skill.extraAttackDuration);
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

        //������ �ɼ�
        Vector2 direction = (target.transform.position - transform.position).normalized;
        int damage = Mathf.RoundToInt(_player.CharStat.GetDamage() * _skill.damageMultiplier); //������ ���� ����.
        target.HealthCompo.ApplyDamage(damage, direction, _skill.knockbackPower, GameManager.Instance.Player);

        //if (_skill.canAilment)
        //{
        //    target.HealthCompo.SetAilment(Ailment.Chilled, _skill.ailmentTime, 0);
        //}

        //������ �ٶ����� �ҵ� ��ų �ǵ�� �ߵ���Ű��.(�ҵ�� UseSkill�� �Ƚ�)
        //SkillManager.Instance.UseSkillFeedback(PlayerSkill.Sword);
    }

    public override void Init()
    {

    }
}
