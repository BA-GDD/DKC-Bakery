using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXManager : MonoBehaviour
{
    //공격시 이펙트 나오게 설정
    [SerializeField] private ParticleSystem[] _blades;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _player.OnStartAttack += PlayBlade;
        _player.OnEndAttack += StopBlade;
    }
    private void PlayBlade(int comboCount)
    {
        _blades[comboCount].Play();
    }
    private void StopBlade()
    {
        foreach (var particle in _blades)
        {
            particle.Simulate(0);
            particle.Stop();
        }
    }
}
