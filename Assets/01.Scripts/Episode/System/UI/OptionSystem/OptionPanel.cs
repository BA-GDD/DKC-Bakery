using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public void ActivePanel(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
