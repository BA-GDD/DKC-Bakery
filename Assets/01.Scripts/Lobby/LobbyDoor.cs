using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public enum DoorType
{
    OutSide,
    Bakery
}

public class LobbyDoor : MonoBehaviour
{
    [SerializeField] private DoorType _myDoorType;
    [SerializeField] private TextMeshPro _guideText;
    [SerializeField] private float _upperValue;
    [SerializeField] private float _easingTime;
    protected bool _isInit;

    private Vector2 _normaPos;
    private Vector2 _apearPos;

    private Sequence _guideSeq;

    private void Awake()
    {
        _normaPos = _guideText.transform.position;
        _apearPos = _guideText.transform.position + new Vector3(0, _upperValue);
    }

    protected virtual void DoorOpen()
    {
        switch (_myDoorType)
        {
            case DoorType.OutSide:
                {
                    SceneManager.LoadScene("MapScene");
                }
                break;
            case DoorType.Bakery:
                {

                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _guideText.transform.position = _apearPos;
        _isInit = true;

        _guideSeq = DOTween.Sequence();
        _guideSeq.Append(_guideText.transform.DOMove(_normaPos, _easingTime));
        _guideSeq.Join(_guideText.DOFade(1, _easingTime));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInit = false;
        _guideSeq = DOTween.Sequence();
        _guideSeq.Append(_guideText.transform.DOMove(_apearPos, _easingTime));
        _guideSeq.Join(_guideText.DOFade(0, _easingTime));
    }

}
