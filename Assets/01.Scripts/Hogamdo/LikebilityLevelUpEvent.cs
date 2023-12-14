using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LikebilityLevelUpEvent : MonoBehaviour
{
    [SerializeField] private TextMeshPro _lvText;

    public void HandleLevelUpObserver(int level)
    {
        _lvText.text = $"Lv.{level + 1}";
    }
}
