using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamagePopupText : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmp;
    public void PopUpDamage(int damage, Vector2 pos, bool isCritical)
    {
        Color textColor = isCritical ? Color.white : Color.red;

        _tmp.color = textColor;
        _tmp.fontSize = isCritical ? 6 : 10;
        _tmp.text = damage.ToString();

        transform.position = pos + Random.insideUnitCircle;

        transform.DOMoveY(transform.position.y + 10, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}
