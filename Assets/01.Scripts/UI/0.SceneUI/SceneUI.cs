using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDefine;

public class SceneUI : MonoBehaviour
{
    [SerializeField] private UIScreenType _myType;
    public UIScreenType ScreenType => _myType;

    public virtual void SceneUIStart()
    {

    }

    public virtual void SceneUIEnd()
    {

    }
}
