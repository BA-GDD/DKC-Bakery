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
        Color textColor = isCritical ? Color.red : Color.white;

        _tmp.color = textColor;
        _tmp.fontSize = isCritical ? 10 : 6;
        _tmp.text = damage.ToString();

        Vector3 myPos = pos + Random.insideUnitCircle*0.5f;
        myPos.z = -5;
        transform.position = myPos;

        transform.DOMoveY(transform.position.y + 2f, 0.5f).OnComplete(() => Destroy(gameObject));
    }
}
