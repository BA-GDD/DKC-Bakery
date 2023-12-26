using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public Transform attackChecker;
    public float attackCheckRadius;

    public Vector2 knockbackPower;

    [SerializeField] private int _maxHitCount = 5; //�ִ�� ���� �� �ִ� �� ����
    public LayerMask whatIsEnemy;
    private Collider2D[] _hitResult;

    private Entity _owner;
    private bool _castByCloneSkill;
    private void Awake()
    {
        _hitResult = new Collider2D[_maxHitCount];
    }

    public void SetOwner(Entity owner, bool castByCloneSkill)
    {
        _owner = owner;
        _castByCloneSkill = castByCloneSkill;
    }

    public virtual bool CastDamage()
    {
        int cnt = Physics2D.OverlapCircle(attackChecker.position, attackCheckRadius, new ContactFilter2D { layerMask = whatIsEnemy, useLayerMask = true }, _hitResult);

        //�̰� ���� ���̴��� ��� �����µ� ������ ����Ƽ���� ����������..����...
        //Physics2D.OverlapCircleAll(attackChecker.position, attackCheckRadius, whatIsEnemy);

        for (int i = 0; i < cnt; ++i)
        {
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;
            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _owner.CharStat.GetDamage();
                if (_castByCloneSkill)
                {
                    damage = Mathf.RoundToInt(damage * SkillManager.Instance.GetSkill<CloneSkill>().damageMultiplier);
                }
<<<<<<< Updated upstream
                Debug.Log(damage);
=======

>>>>>>> Stashed changes
                health.ApplyDamage(damage, direction, knockbackPower, _owner);
                SetAilmentByStat(health);
            }
        }

        return cnt > 0;
    }

    private void SetAilmentByStat(IDamageable targetHealth)
    {
        CharacterStat stat = _owner.CharStat; //������ ��������
        //float duration = stat.ailmentTimeMS.GetValue() * 0.001f;

        //if (stat.canIgniteByMelee && stat.CanAilment(Ailment.Ignited)) //��ȭ ����
        //{
        //    int damage = stat.GetDotDamage(Ailment.Ignited);
        //    targetHealth.SetAilment(Ailment.Ignited, duration, damage);
        //}

        //if (stat.canChillByMelee && stat.CanAilment(Ailment.Chilled))
        //{
        //    int damage = stat.GetDotDamage(Ailment.Chilled);
        //    targetHealth.SetAilment(Ailment.Chilled, duration, damage);
        //}

        //if (stat.canShockByMelee && stat.CanAilment(Ailment.Shocked))
        //{
        //    int damage = stat.GetDotDamage(Ailment.Shocked);
        //    targetHealth.SetAilment(Ailment.Shocked, duration, damage);
        //}
    }

    protected virtual void OnDrawGizmos()
    {
        if (attackChecker != null)
            Gizmos.DrawWireSphere(attackChecker.position, attackCheckRadius);
    }
}