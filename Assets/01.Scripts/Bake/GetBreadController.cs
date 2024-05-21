using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBreadController : MonoBehaviour
{
    [SerializeField] private GameObject _grapicsElementParentObj;
    private IBakingProductionObject[] _productionGraphicsObjArr;

    private void Awake()
    {
        _productionGraphicsObjArr =
        _grapicsElementParentObj.GetComponentsInChildren<IBakingProductionObject>();

    }

    public void OnProduction()
    {
        foreach(var production in _productionGraphicsObjArr)
        {
            production.OnProduction();
            Debug.Log(production);
        }
    }

    public void ExitProduction()
    {
        foreach (var production in _productionGraphicsObjArr)
        {
            production.ExitProduction();
        }
    }
}
