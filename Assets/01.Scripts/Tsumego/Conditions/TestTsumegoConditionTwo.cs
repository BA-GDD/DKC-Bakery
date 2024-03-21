using UnityEngine;

[CreateAssetMenu(menuName = "SO/Tsumego/Test2")]
public class TestTsumegoConditionTwo : TsumegoCondition
{
    public override bool CheckCondition()
    {
        if (Time.deltaTime >= 200f)
        {
            Debug.Log("200초 지남");
            return true;
        }
        else
        {
            Debug.Log($"20초 안 지남 {Time.time}");
            return false;
        }
    }
}
