using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/Test2")]
public class TestTsumegoConditionTwo : TsumegoCondition
{
    public override bool CheckCondition()
    {
        if (Time.time >= 20f)
        {
            Debug.Log("20�� ����");
            return true;
        }
        else
        {
            Debug.Log($"20�� �� ���� {Time.time}");
            return false;
        }
    }
}
