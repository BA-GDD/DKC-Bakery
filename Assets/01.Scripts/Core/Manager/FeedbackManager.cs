using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.InputSystem;

using Random = UnityEngine.Random;

public class FeedbackManager : MonoSingleton<FeedbackManager>
{
    //[SerializeField] private CinemachineImpulseSource _impulseSource;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [SerializeField] private float _cinemachineSpeed = 1.0f;

    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    private bool _shakingInDuration = false;

    // 시간 관련
    private float _limitTime;
    private float _currentTime;

    public void ShakeScreen(float shakeValue)
    {/*
        Vector3 randomVector = new Vector3(Random.Range(-shakeValue, shakeValue), Random.Range(-shakeValue, shakeValue), Random.Range(-shakeValue, shakeValue));
        //_impulseSource.m_DefaultVelocity = Vector3.one * MaestrOffice.GetPlusOrMinus() * shakeValue;
        _impulseSource.m_DefaultVelocity = randomVector;
        _impulseSource.GenerateImpulse();*/

        if(_multiChannelPerlin == null)
            _multiChannelPerlin = _cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _multiChannelPerlin.m_FrequencyGain = shakeValue;
        _multiChannelPerlin.m_AmplitudeGain = shakeValue;
    }

    public void ShakeScreen(float shakeValue, float shakeTime)
    {
        //_impulseSource.m_DefaultVelocity = Vector3.one * MaestrOffice.GetPlusOrMinus() * shakeValue;

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

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShakeScreen(5);
        }
        
        
    }

    private void FixedUpdate()
    {
        if (_multiChannelPerlin != null)
        {
            if (_multiChannelPerlin.m_AmplitudeGain >= 0.1f)
            {
                _multiChannelPerlin.m_AmplitudeGain -= Time.deltaTime * _cinemachineSpeed;
            }
            if (_multiChannelPerlin.m_FrequencyGain >= 0.1f)
            {
                _multiChannelPerlin.m_FrequencyGain -= Time.deltaTime * _cinemachineSpeed;
            }
        }

        if (_shakingInDuration)
        {
            _currentTime += Time.fixedDeltaTime;
            //_impulseSource.GenerateImpulse();
            if(_currentTime >= _limitTime)
            {
                _shakingInDuration = false;
            }
        }
    }
}
