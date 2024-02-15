using UnityEngine;
using EpisodeDialogueDefine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class CharacterStandard : MonoBehaviour
{
    [Header("수치")]
    [SerializeField] private float _activeTime;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _exitTime;

    [Header("셋팅값")]
    [SerializeField] private Image _characterDraw;
    [SerializeField] private Sprite[] _faceGroup;
    [SerializeField] private Vector2[] _movePointGroup;
    [SerializeField] private Vector2[] _exitPointGroup;

    private FaceType _currentFaceType;
    private bool _currentActive;
    private MoveType _alreadyInPos;
    private ExitType _alreadyOutPos;

    public void SetFace(FaceType faceType)
    {
        if (_currentFaceType == faceType) return;

        //_characterDraw.sprite = _faceGroup[(int)faceType];
        _currentFaceType = faceType;
    }

    public void SetActive(bool isActive)
    {
        if (_currentActive == isActive) return;

        _characterDraw.DOFade(Convert.ToInt32(isActive), _activeTime);
        _currentActive = isActive;
    }

    public void CharacterShake()
    {
        transform.DOShakePosition(0.4f, 15f, 20);
    }

    public void MoveCharacter(MoveType moveType)
    {
        if (_alreadyInPos == moveType || moveType == MoveType.None) return;

        transform.DOLocalMove(_movePointGroup[(int)moveType - 1], _moveTime);
    }

    public void ExitCharacter(ExitType exitType)
    {
        if (_alreadyOutPos == exitType || exitType == ExitType.None) return;
        transform.DOLocalMove(_exitPointGroup[(int)exitType - 1], _exitTime);
    }
}
