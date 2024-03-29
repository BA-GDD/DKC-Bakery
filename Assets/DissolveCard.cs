using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DissolveCard : MonoBehaviour
{
    private void OnEnable()
    {
        Material mat = GetComponent<Image>().material;

        StartCoroutine(Dissolve(mat));
    }

    private IEnumerator Dissolve(Material mat)
    {
        float dissolve = -1.0f;
        while (true)
        {
            yield return null;

            if (dissolve >= 0.0f)
            {
                dissolve = 0.0f;
                break;
            }

            dissolve += 0.01f;
            mat.SetFloat("_dissolve_amount", dissolve);
        }
        mat.SetFloat("_dissolve_amount", 1.0f);
    }
}
