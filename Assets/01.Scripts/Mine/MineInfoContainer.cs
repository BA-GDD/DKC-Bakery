using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineInfoContainer : MonoBehaviour
{
    [SerializeField] private List<MineInfo> _infoContainer = new List<MineInfo>();

    public MineInfo GetInfoByFloor(int floor)
    {
        if(floor < 0 || floor >= _infoContainer.Count)
        {
            Debug.LogError("�׷� ���� ���µ��?");
            return null;
        }
        return _infoContainer[floor];
    }
}
