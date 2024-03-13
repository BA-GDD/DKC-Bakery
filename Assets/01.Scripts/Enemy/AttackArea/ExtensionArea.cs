using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionArea : AttackAreaRenderer
{
    [SerializeField] private float maxRadius;
    [SerializeField] private float endTime;
    public override void Render()
    {
        base.Render();
        StartCoroutine(ExtensionCor());
    }


    public IEnumerator ExtensionCor()
    {
        float curTime = 0;

        while (true)
        {
            curTime += Time.deltaTime;
            float radius = Mathf.Lerp(0, maxRadius, curTime / endTime);
            spriteRenderers.gameObject.transform.localScale = new Vector3(radius*2, radius*2);

            yield return null;
        }
    }

}
