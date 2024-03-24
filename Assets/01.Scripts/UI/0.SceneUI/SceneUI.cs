using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class SceneUI : MonoBehaviour
{
    [SerializeField] private SceneType _myType;
    public SceneType ScreenType => _myType;


    public virtual void SceneUIStart()
    {

    }

    public virtual void SceneUIEnd()
    {

    }
}
