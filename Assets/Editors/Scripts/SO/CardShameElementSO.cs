using CardDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum CardShameType
{
    Damage,
    Buff,
    Debuff,
    Cost,
    Range,
    Time
}

[Serializable]
public class CardShameData 
{
    public CardShameType cardShameType;
    public int currentShame;
    public int afterShame;
    public string info;
}

[Serializable]
public struct IncreaseValuePerLevel
{
    public CardShameType shameType;
    public int perValue;
}

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "SO/Card/Data")]
#endif
public class CardShameElementSO : ScriptableObject
{
    [Header("카드 레벨")]
    public int cardLevel = 1;
    public float cardExp;

    [Header("카드 수치")]
    public List<IncreaseValuePerLevel> increaseValuePerLevel;
    public List<SEList<CardShameData>> cardShameDataList = new ();
}


#if UNITY_EDITOR
[CustomEditor(typeof(CardShameElementSO), true)]
public class CardShameElementShameEditor : Editor
{
    private CardShameElementSO _shameDataSO;
    private int _lastCountOfCardShameDataList;

    private void OnEnable()
    {
        _shameDataSO = (CardShameElementSO)target;
        _lastCountOfCardShameDataList = _shameDataSO.cardShameDataList.Count;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(_lastCountOfCardShameDataList < _shameDataSO.cardShameDataList.Count)
        {
            foreach(var inc in  _shameDataSO.increaseValuePerLevel)
            {
                CardShameData data = _shameDataSO.cardShameDataList[_lastCountOfCardShameDataList].list.
                                     FirstOrDefault(x => x.cardShameType == inc.shameType);

                data.currentShame += inc.perValue;
                data.afterShame += inc.perValue;
            }
        }

        _lastCountOfCardShameDataList = _shameDataSO.cardShameDataList.Count;

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_shameDataSO);
        }
    }
}
#endif

