using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Enemy : Entity
{
    [CustomEditor(typeof(Enemy), true)]
    public class EnemyEditor : EntityEditor
    {
        private Enemy _enemy;
        private bool _stateSetting;
        private bool _attackSetting;
        

        protected override void OnEnable()
        {
            base.OnEnable();
            _enemy = (Enemy)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var boldtext = new GUIStyle(GUI.skin.label);
            boldtext.fontStyle = FontStyle.Bold;


            _stateSetting = EditorGUILayout.Foldout(_stateSetting, "Setting", true);
            if(_stateSetting)
            {
                _enemy.moveSpeed = EditorGUILayout.FloatField("MoveSpeed", _enemy.moveSpeed);
                _enemy.idleTime = EditorGUILayout.FloatField("IdleTime", _enemy.idleTime);
                _enemy.battleTime = EditorGUILayout.FloatField("BattleTime", _enemy.battleTime);
            }

            _attackSetting = EditorGUILayout.Foldout(_attackSetting, "AttackSetting", true);
            if(_attackSetting)
            {
                _enemy.runAwayDistance = EditorGUILayout.FloatField("RunAwayDistance", _enemy.runAwayDistance);
                _enemy.attackDistance = EditorGUILayout.FloatField("AttackDistance", _enemy.attackDistance);
                _enemy.attackCooldown = EditorGUILayout.FloatField("AttackCooldown", _enemy.attackCooldown);
            }
        }
    }

    [HideInInspector] public int phase;

    [Header("���ð���")]
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float idleTime;
    [HideInInspector] public float battleTime; //�����ð��� �ʰ��ϸ� idle���·� �̵��Ѵ�.

    private float _defaultMoveSpeed;

    [SerializeField] protected LayerMask _whatIsPlayer;
    [SerializeField] protected LayerMask _whatIsObstacle;

    [Header("���ݻ��¼�����")]
    [HideInInspector] public float runAwayDistance;
    [HideInInspector] public float attackDistance;
    [HideInInspector] public float attackCooldown;

    [HideInInspector] public float lastTimeAttacked;

    protected bool _isFrozen = false; //����ִ� ����
    protected bool _isFrozenWithoutTimer = false; //�ð����� ���� ������ ��ų��

    protected int _lastAnimationBoolHash; //���������� ����� �ִϸ��̼� �ؽ�

    protected override void Awake()
    {
        base.Awake();
        _defaultMoveSpeed = moveSpeed;

        //������ ���� ���̵� ����        
        ApplyLevelModifier();
    }

    private void ApplyLevelModifier()
    {
        EnemyStat enemyStat = CharStat as EnemyStat;
        if (enemyStat == null)
        {
            Debug.LogError($"non enemy stat infomation is assigned : {gameObject.name}");
            return;
        }

        //�������� ü�¸� ����. 
        enemyStat.Modify(enemyStat.damage);
        enemyStat.Modify(enemyStat.maxHealth);

        OnHealthBarChanged?.Invoke(HealthCompo.GetNormalizedHealth(), HealthCompo.GetNormalizedHealth()); //�ִ�ġ�� UI����. 
    }

    protected override void Update()
    {
        base.Update();

    }

    public virtual void AssignLastAnimHash(int hashCode)
    {
        _lastAnimationBoolHash = hashCode;
    }

    public int GetLastAnimHash()
    {
        return _lastAnimationBoolHash;
    }

    //���� 50�� �÷��̾ �ִ��� �˻�.
    public virtual RaycastHit2D IsPlayerDetected()
        => Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, runAwayDistance, _whatIsPlayer);

    public virtual bool IsObstacleInLine(float distance)
    {
        return Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, distance, _whatIsObstacle);
    }

    public abstract void AnimationFinishTrigger();

    //���� Ÿ�� ����¡�� �ɷȴٸ�.
    public virtual void FreezeTime(bool isFreeze, bool isFrozenWithoutTimer = false)
    {
        if (isFrozenWithoutTimer)
        {
            _isFrozenWithoutTimer = true; //�ð����Ѿ��� �󸱶��� true�϶���. 
        }

        _isFrozen = isFreeze;
        if (isFreeze)
        {
            Debug.Log("Freezed");
            moveSpeed = 0;
            AnimatorCompo.speed = 0; //�ִϸ��̼� ����. �̵� ����.
        }
        else
        {
            Debug.Log("UnFreezed");
            moveSpeed = _defaultMoveSpeed;
            AnimatorCompo.speed = 1;
            _isFrozenWithoutTimer = false;
        }
    }

    public virtual async void FreezeTimerFor(float delaySec)
    {
        FreezeTime(true); //����
        Debug.Log(delaySec);
        await Task.Delay(Mathf.FloorToInt(delaySec * 1000));

        if (!_isFrozenWithoutTimer)
        {
            FreezeTime(false); //���
        }//���� ��� �����϶��� Ÿ�̸Ӱ� Ǯ�� ���Ѵ�.

    }

    //public override void SlowEntityBy(float percent)
    //{
    //    if (moveSpeed < _defaultMoveSpeed) return; //�ߺ� ���� ����.
    //    moveSpeed *= 1 - percent;
    //    AnimatorCompo.speed *= 1 - percent;
    //}

    //protected override void ReturnDefaultSpeed()
    //{
    //    base.ReturnDefaultSpeed();
    //    moveSpeed = _defaultMoveSpeed;
    //}

    #region counter attack region
    public virtual void OpenCounterAttackWindow()
    {
        _canBeStuned = true;
    }

    public virtual void CloseCounterAttackWindow()
    {
        _canBeStuned = false;
    }

    public virtual bool CanBeStunned()
    {
        if (_canBeStuned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
    #endregion

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * FacingDirection, transform.position.y));
        Gizmos.color = Color.white;
    }
#endif
}