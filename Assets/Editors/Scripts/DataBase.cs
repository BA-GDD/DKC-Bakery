using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    [SerializeField] private LoadableData[] _loadableDatas;

    public void GenerateDatas()
    {
        //foreach (LoadableData ld in _loadableDatas)
        //{
        //    StartCoroutine(ld.StartLoadData());
        //}
    }
}
