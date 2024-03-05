

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentStat
{
    private Dictionary<Ailment, float> _ailmentTimerDictionary;
    private Dictionary<Ailment, int> _ailmentDamageDictionary;


    public Ailment currentAilment; //���� �� ����� ����

    public event Action<Ailment, int> AilmentDamageEvent; //�����̻� ������ ������ �̺�Ʈ
    public event Action<Ailment> EndOFAilmentEvent; // �����̻� ����� �߻�

    private float _igniteTimer;
    private float _igniteDamageCooldown = 0.3f;

    public AilmentStat()
    {
        _ailmentTimerDictionary = new Dictionary<Ailment, float>();
        _ailmentDamageDictionary = new Dictionary<Ailment, int>();

        foreach (Ailment ailment in Enum.GetValues(typeof(Ailment)))
        {
            if (ailment != Ailment.None)
            {
                _ailmentTimerDictionary.Add(ailment, 0f);
                _ailmentDamageDictionary.Add(ailment, 0); //�������� ��Ÿ�� �ʱ�ȭ
            }
        }
    }

    public void UpdateAilment()
    {
        foreach (Ailment ailment in Enum.GetValues(typeof(Ailment)))
        {
            if (ailment == Ailment.None) continue;

            if (_ailmentTimerDictionary[ailment] > 0)
            {
                _ailmentTimerDictionary[ailment] -= Time.deltaTime;
                if (_ailmentTimerDictionary[ailment] <= 0)
                {
                    currentAilment ^= ailment; //XOR�� ���ְ�
                    EndOFAilmentEvent?.Invoke(ailment); //���� �˸�.
                }
            }
        }

        //DOT ���������� ���⼭ ó��.
        IgniteTimer();
    }

    private void IgniteTimer() //��ȭ�� ��� ƽ�������� ��� �ϴϱ�.
    {
        if ((currentAilment & Ailment.Ignited) == 0) return;

        _igniteTimer += Time.deltaTime;
        if (_ailmentTimerDictionary[Ailment.Ignited] > 0 && _igniteTimer > _igniteDamageCooldown)
        {
            _igniteTimer = 0;
            AilmentDamageEvent?.Invoke(Ailment.Ignited, _ailmentDamageDictionary[Ailment.Ignited]);
        }
    }

    //Ư�� ������� �����ϴ��� üũ
    public bool HasAilment(Ailment ailment)
    {
        return (currentAilment & ailment) > 0;
    }

    public void ApplyAilments(Ailment value, float duration, int damage)
    {
        currentAilment |= value; //���� �����̻� �߰� �����̻� �������

        //�����̻� ���� ���� �ֵ��� �ð� �������ְ�. 
        if ((value & Ailment.Ignited) > 0)
        {
            SetAilment(Ailment.Ignited, duration: duration, damage: damage);
        }
        else if ((value & Ailment.Chilled) > 0)
        {
            SetAilment(Ailment.Chilled, duration, damage);
        }
        else if ((value & Ailment.Shocked) > 0)
        {
            SetAilment(Ailment.Shocked, duration, damage);
        }
    }

    //����ȿ���� ���ӽð� ����
    private void SetAilment(Ailment ailment, float duration, int damage)
    {
        _ailmentTimerDictionary[ailment] = duration;
        _ailmentDamageDictionary[ailment] = damage;
    }

}