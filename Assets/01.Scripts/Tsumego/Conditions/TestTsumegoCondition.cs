using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/AllKill")]
public class TestTsumegoCondition : TsumegoCondition
{
    public override bool CheckCondition()
    {
<<<<<<< HEAD
        Enemy[] earr = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy e in earr)
=======
        if(Time.time >= 10f)
>>>>>>> parent of 8b20a26 (0321 머지 전 커밋)
        {
            if(!e.HealthCompo.isDead) return false;
        }
        return true;
    }
}
