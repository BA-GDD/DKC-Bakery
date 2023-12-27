using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ExplosionScreen : MonoBehaviour
{

    //디버그용 스크립트
    private Rigidbody[] _explosionRbs;

    [SerializeField] private GameObject _renderCam;

    [SerializeField] private GameObject _shatterEffectObj;
    [SerializeField] private Transform _explosionTrm;
    [SerializeField] private ParticleSystem _swordScreenEffect;

    [SerializeField] private float _explosionPow;
    [SerializeField] private float _explosionRad;

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            
            StartCoroutine(Shatter());
        }
    }

    private IEnumerator Shatter()
    {

        _renderCam.SetActive(true);
        yield return new WaitForEndOfFrame();
        _renderCam.SetActive(false);

        _swordScreenEffect.Play();
        yield return new WaitForSeconds(1.6f);

        var obj = Instantiate(_shatterEffectObj, _renderCam.transform.position, Quaternion.identity);
        obj.transform.position += new Vector3(0,0,5);
        obj.transform.rotation = Quaternion.Euler(0, 0, 180);
        _explosionRbs = obj.GetComponentsInChildren<Rigidbody>();

        foreach(var rb in _explosionRbs)
        {
            rb.AddExplosionForce(_explosionPow, _explosionTrm.position, _explosionRad);
        }

        yield return new WaitForSeconds(3.0f);

        Destroy(obj);
    }
}
