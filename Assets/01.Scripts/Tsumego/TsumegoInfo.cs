using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Tsumego Info", menuName = "SO/Tsumego/Info")]
public class TsumegoInfo : ScriptableObject
{
    public bool IsClear;
    public string Name;

    public List<TsumegoCondition> Conditions;

#if UNITY_EDITOR
    [ContextMenu("Add Condition")]
    private void AddCondition()
    {
        TsumegoCondition condition = ScriptableObject.CreateInstance<TsumegoCondition>();
        condition.name = "New Condition";
        condition.Init(this);
        Conditions.Add(condition);

        AssetDatabase.AddObjectToAsset(condition, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(condition);
    }

    [ContextMenu("Test")]
    private void Test()
    {
        TsumegoCondition condition = ScriptableObject.CreateInstance<TestTsumegoCondition>();
        condition.name = "New Condition";
        condition.Init(this);
        Conditions.Add(condition);

        AssetDatabase.AddObjectToAsset(condition, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(condition);
    }

    [ContextMenu("Test2")]
    private void Test2()
    {
        TsumegoCondition condition = ScriptableObject.CreateInstance<TestTsumegoConditionTwo>();
        condition.name = "New Condition";
        condition.Init(this);
        Conditions.Add(condition);

        AssetDatabase.AddObjectToAsset(condition, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(condition);
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Delete all")]
    private void DeleteAll()
    {
        for(int i = Conditions.Count; i-- > 0;)
        {
            TsumegoCondition temp = Conditions[i];

            Conditions.Remove(temp);
            Undo.DestroyObjectImmediate(temp);
        }
        AssetDatabase.SaveAssets();
    }
#endif
}
