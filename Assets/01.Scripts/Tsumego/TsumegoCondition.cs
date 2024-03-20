using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "SO/Tsumego/Condition")]
public class TsumegoCondition : ScriptableObject
{
    public TsumegoInfo MyInfo;
    public string Name;

#if UNITY_EDITOR
    public void Init(TsumegoInfo info)
    {
        MyInfo = info;
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Rename to name")]
    private void Rename()
    {
        this.name = Name;
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }
#endif

#if UNITY_EDITOR
    [ContextMenu("Delete this")]
    private void Delete()
    {
        MyInfo.Conditions.Remove(this);
        Undo.DestroyObjectImmediate(this);
        AssetDatabase.SaveAssets();
    }
#endif

    public virtual bool CheckCondition() { return false; }
}
