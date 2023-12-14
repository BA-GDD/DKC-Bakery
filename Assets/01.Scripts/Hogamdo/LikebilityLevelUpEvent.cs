using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LikebilityLevelUpEvent : MonoBehaviour
{
    [SerializeField] private TextMeshPro _lvText;
    private int _outputLevel;

    public void HandleLevelUpObserver(int level)
    {
        _outputLevel = level + 1;

        MawangManager.Instanace.currentLikeability = _outputLevel;
        _lvText.text = $"Lv.{_outputLevel}";
    }
}
