using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/Test2")]
public class TestTsumegoConditionTwo : TsumegoCondition
{
    public override bool CheckCondition()
    {
        if (Time.deltaTime >= 200f)
        {
            Debug.Log("200�� ����");
            return true;
        }
        else
        {
            Debug.Log($"20�� �� ���� {Time.time}");
            return false;
        }
    }
}
