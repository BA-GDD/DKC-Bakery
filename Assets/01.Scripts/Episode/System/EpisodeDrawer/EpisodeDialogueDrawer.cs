using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EpisodeDialogueDefine;
using TMPro;
using System;

public class EpisodeDialogueDrawer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTextTmp;
    private string _nameText;
    private string _nameTextPro
    {
        get
        {
            return _nameText;
        }
        set
        {
            _nameText = value;
            _nameTextTmp.text = _nameText;
        }
    }

    [SerializeField] private TextMeshProUGUI _syntexTextTmp;
    private string _syntexText;
    private string _syntexTextPro
    {
        get
        {
            return _syntexText;
        }
        set
        {
            _syntexText = value;
            _syntexTextTmp.text = _syntexText;
        }
    }

    [SerializeField] private SpriteRenderer _backGround;
    [SerializeField] private List<Sprite> _backGroundList = new List<Sprite>();
    private BackGroundType _bgType;

    public void HandleStandardElementDraw(string name, string syntex, BackGroundType bgType)
    {
        _nameTextPro = name;
        _syntexTextPro = syntex;

        if (_bgType == bgType) return;
        UpdateBackGround(bgType);
    }

    private void UpdateBackGround(BackGroundType bgType)
    {
        _backGround.sprite = _backGroundList[(int)bgType];
        _bgType = bgType;
    }
}
