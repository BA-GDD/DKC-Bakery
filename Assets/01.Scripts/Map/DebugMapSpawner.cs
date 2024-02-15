using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMapSpawner : MonoBehaviour
{
    public Transform mapTrmsParent;
    public Transform btnParent;

    //include prefabs
    public GameObject[] objs;
    private List<GameObject> _curObjects = new List<GameObject>();

    private int _curStageIndex = 0;

    private Button[] _btns;

    private void Awake()
    {
        _btns = btnParent.GetComponentsInChildren<Button>();
        
        for(int i = 0; i < _btns.Length; ++i)
        {
            int index = i;
            _btns[index].onClick.AddListener(() => StageIns(index));
        }
    }

    private void Start()
    {
        foreach(var o in objs)
        {
            GameObject obj = Instantiate(o);
            obj.transform.position = Vector3.zero;
            obj.transform.SetParent(mapTrmsParent);

            _curObjects.Add(obj);
            obj.SetActive(false);
        }
    }

    //Button Event
    public void StageIns(int index)
    {
        _curObjects[_curStageIndex].SetActive(false);
        _curObjects[index].SetActive(true);
    }
}
