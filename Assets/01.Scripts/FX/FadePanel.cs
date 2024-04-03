using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadePanel : MonoBehaviour
{
    public Vector2 offset = Vector2.zero;
    public float radius = 0.0f;
    public Shader shader;

    private Material material;

    private void Awake()
    {
        radius = 2.0f;

        if (material == null) material = new Material(shader);

        GetComponent<Image>().material = material;

        material.SetFloat("_radius", 5.0f);
    }

    private void Fade()
    {
        material.DOFloat(0.0f, Shader.PropertyToID("_radius"), 1.0f);
    }

    private void DeFade()
    {
        material.DOFloat(3.0f, Shader.PropertyToID("_radius"), 2.0f);
    }

    public Coroutine StartFade()
    {
        return StartCoroutine(FadeCo());
    }

    private IEnumerator FadeCo()
    {
        Fade();
        yield return new WaitForSeconds(1.5f);
        DeFade();
    }
}
