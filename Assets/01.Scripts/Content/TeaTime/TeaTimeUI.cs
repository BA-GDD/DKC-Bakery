using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TeaTimeUI : SceneUI
{
    [SerializeField] private EatRange _eatRange;
    public EatRange EatRange => _eatRange;
    [SerializeField] private TeaTimeCreamStand _creamStand;
    public TeaTimeCreamStand TeaTimeCreamStand => _creamStand;
    [SerializeField] private CakeCollocation _cakeCollocation;
    public CakeCollocation CakeCollocation => _cakeCollocation;

    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private SpriteRenderer sprite;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            director.Play();
        }
    }
}
