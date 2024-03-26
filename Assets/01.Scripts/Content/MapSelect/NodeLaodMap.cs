using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLaodMap : MonoBehaviour
{
    [SerializeField] private Transform _deckSelectParent;

    [SerializeField] private Transform _bubbleTrm;
    public Transform BubbleTrm => _bubbleTrm;

    public void ExitLoadMap()
    {
        MapManager.Instanace.ActiveLoadMap(false);
    }
}
