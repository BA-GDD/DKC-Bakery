using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] private CloneSkillContoller _clonePrefab;
    [SerializeField] private bool _createCloneOnDashStart;
    [SerializeField] private bool _createCloneOnDashEnd;
    [SerializeField] private bool _createCloneOnCounterAttack;

    public float cloneDuration;

    public float findEnemyRadius = 5f;

    public void CreateClone(Transform originTrm, Vector3 offset)
    {
        CloneSkillContoller newClone = Instantiate(_clonePrefab);
        newClone.SetUpClone(this, originTrm, offset);
    }

    public void CraeteCloneOnDashStart()
    {
        if (_createCloneOnDashStart)
        {
            CreateClone(_player.transform, Vector3.zero);
        }
    }
    public void CraeteCloneOnDashEnd()
    {
        if (_createCloneOnDashEnd)
        {
            CreateClone(_player.transform, Vector3.zero);
        }
    }
}
