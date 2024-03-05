using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class FeedbackManager : MonoSingleton<FeedbackManager>
{
    [SerializeField] private CinemachineImpulseSource _impulseSource;
    private bool _shakingInDuration = false;

    // 시간 관련
    private float _limitTime;
    private float _currentTime;

    public void ShakeScreen(float shakeValue)
    {
        _impulseSource.m_DefaultVelocity = Vector3.one * MaestrOffice.GetPlusOrMinus() * shakeValue;
        _impulseSource.GenerateImpulse();
    }

    public void ShakeScreen(float shakeValue, float shakeTime)
    {
        _impulseSource.m_DefaultVelocity = Vector3.one * MaestrOffice.GetPlusOrMinus() * shakeValue;

        _currentTime = 0;
        _limitTime = shakeTime;
        _shakingInDuration = true;
    }

    public void FreezeTime(float freezeValue, float freezeTime)
    {
        StartCoroutine(FreezeCo(freezeTime));
        Time.timeScale = freezeValue;
    }

    private IEnumerator FreezeCo(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        if(_shakingInDuration)
        {
            _currentTime += Time.fixedDeltaTime;
            _impulseSource.GenerateImpulse();
            if(_currentTime >= _limitTime)
            {
                _shakingInDuration = false;
            }
        }
    }
}
