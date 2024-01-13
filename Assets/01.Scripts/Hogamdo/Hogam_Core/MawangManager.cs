using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawangManager : MonoBehaviour
{
    private static MawangManager _instance;
    public static MawangManager Instanace
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType<MawangManager>();
            if (_instance == null)
            {
                Debug.LogError("Not Exist UIManager");
            }
            return _instance;
        }
    }

    [HideInInspector] public int currentLikeability;
}
