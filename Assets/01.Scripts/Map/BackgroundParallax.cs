using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BackGroundLayer
{
    public Transform transform;
    public Vector2 value;
}

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private List<BackGroundLayer> backgrounds;

    private Transform _camTrm;

    private void Start()
    {
        _camTrm = Camera.main.transform;
    }
    private void FixedUpdate()
    {
        Vector2 camPos = _camTrm.position;
        //foreach (var bg in backgrounds)
        for(int i = 0; i < backgrounds.Count; ++i)
        {
            //Vector2 valueVec = bg.value;
            Vector2 valueVec = backgrounds[i].value;
            Vector2 pos = new Vector2(camPos.x * valueVec.x, camPos.y * valueVec.y);
            //bg.transform.position = pos;
            backgrounds[i].transform.position = pos;
        }
    }
}
