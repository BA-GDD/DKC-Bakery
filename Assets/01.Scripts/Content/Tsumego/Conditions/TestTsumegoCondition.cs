using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/AllKill")]
public class TestTsumegoCondition : TsumegoCondition
{
    public override bool CheckCondition()
    {
        Enemy[] earr = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy e in earr)
        {
            if(!e.HealthCompo.IsDead) return false;
        }
        return true;
    }
}
