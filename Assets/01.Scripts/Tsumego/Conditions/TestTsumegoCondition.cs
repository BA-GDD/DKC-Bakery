using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/Test")]
public class TestTsumegoCondition : TsumegoCondition
{
    public override bool CheckCondition()
    {
        if(Time.time >= 10f)
        {
            Debug.Log("10�� ����");
            return true;
        }
        else
        {
            Debug.Log($"10�� �� ���� {Time.time}");
            return false;
        }
    }
}
