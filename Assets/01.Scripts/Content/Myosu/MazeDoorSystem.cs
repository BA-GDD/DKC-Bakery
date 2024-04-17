using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeDoorSystem : MonoBehaviour
{
    [SerializeField] private Image _wallImg;
    private MazeDoor[] _mazeDoorArr;

    private void Awake()
    {
        _mazeDoorArr = GetComponentsInChildren<MazeDoor>();
    }

    public void SelectDoor(MazeDoor mazeDoor)
    {
        foreach (MazeDoor md in _mazeDoorArr)
        {
            md.CanInteractible = false;
            if (md != mazeDoor)
            {
                md.Visual.DOFade(0, 0.2f);
            }
        }
    }    

    public void HoverDoor(MazeDoor mazeDoor)
    {
        _wallImg.DOFade(0.8f, 0.2f);
        foreach(MazeDoor md in _mazeDoorArr)
        {
            if (md != mazeDoor)
            {
                md.Visual.DOFade(0.2f, 0.3f);
            }
        }
    }

    public void UnHoverDoor(MazeDoor mazeDoor)
    {
        _wallImg.DOFade(0.5f, 0.2f);
        foreach (MazeDoor md in _mazeDoorArr)
        {
            if (md != mazeDoor)
            {
                md.Visual.DOFade(1f, 0.3f);
            }
        }
    }
}
