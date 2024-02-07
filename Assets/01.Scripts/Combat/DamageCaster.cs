using UnityEditor;
using UnityEngine;

public enum DamageCasterShare { Circle, Box }

public class DamageCaster : MonoBehaviour
{
    [CustomEditor(typeof(DamageCaster))]
    public class DamageCasterEditor : Editor
    {
        private DamageCaster _damageCaster;

        private void OnEnable()
        {
            _damageCaster = (DamageCaster)target; //타겟 가져오고
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            switch (_damageCaster.share)
            {

                case DamageCasterShare.Circle:
                    _damageCaster.attackCheckRadius = EditorGUILayout.FloatField("attackCheckRadius", _damageCaster.attackCheckRadius);
                    break;
                case DamageCasterShare.Box:
                    _damageCaster.attackCheckSize = EditorGUILayout.Vector2Field("attackCheckSize", _damageCaster.attackCheckSize);
                    break;
            }

            //GUI상에서 변경이 감지되었다면 새로 그리길 명령.
            if (GUI.changed)
            {
                EditorUtility.SetDirty(_damageCaster);
            }
        }
    }
    public DamageCasterShare share;

    public Transform attackChecker;

    [HideInInspector] public float attackCheckRadius;

    [HideInInspector] public Vector2 attackCheckSize;

    public Vector2 knockbackPower;

    [SerializeField] private int _maxHitCount = 5; //�ִ�� ���� �� �ִ� �� ����
    public LayerMask whatIsEnemy;
    private Collider2D[] _hitResult;

    private Entity _owner;
    private void Awake()
    {
        _hitResult = new Collider2D[_maxHitCount];
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public virtual bool CastDamage()
    {
        int cnt = 0;
        switch (share)
        {
            case DamageCasterShare.Circle:
                cnt = Physics2D.OverlapCircle(attackChecker.position, attackCheckRadius, new ContactFilter2D { layerMask = whatIsEnemy, useLayerMask = true }, _hitResult);
                break;
            case DamageCasterShare.Box:
                cnt = Physics2D.OverlapBox(attackChecker.position, attackCheckSize, 0f, new ContactFilter2D { layerMask = whatIsEnemy, useLayerMask = true }, _hitResult);
                break;
        }

        //�̰� ���� ���̴��� ��� �����µ� ������ ����Ƽ���� ����������..����...
        //Physics2D.OverlapCircleAll(attackChecker.position, attackCheckRadius, whatIsEnemy);

        for (int i = 0; i < cnt; ++i)
        {
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;
            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _owner.CharStat.GetDamage();
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
        {
            switch (share)
            {
                case DamageCasterShare.Circle:
                    Gizmos.DrawWireSphere(attackChecker.position, attackCheckRadius);
                    break;
                case DamageCasterShare.Box:
                    Gizmos.DrawWireCube(attackChecker.position, attackCheckSize);
                    break;
            }
        }
    }
}
