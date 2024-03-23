using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentStat
{
    private Dictionary<AilmentEnum, Ailment> _ailmentDictionary;


    public AilmentEnum currentAilment; //���� �� ����� ����

    public event Action<AilmentEnum> EndOFAilmentEvent; // �����̻� ����� �߻�

    public AilmentStat(Health _health)
    {
        _ailmentDictionary = new Dictionary<AilmentEnum, Ailment>();

        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (ailment == AilmentEnum.None) continue;

            Type t = Type.GetType($"{ailment}Ailment");

            Ailment a = Activator.CreateInstance(t, this, _health, ailment) as Ailment;
            _ailmentDictionary.Add(ailment, a);
        }
    }

    public void UpdateAilment()
    {
        AilmentDamage();
        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (ailment == AilmentEnum.None) continue;
            
            if(HasAilment(ailment))
                _ailmentDictionary[ailment].Update();
        }
    }
    public void CuredAilment(AilmentEnum ailment)
    {
        currentAilment ^= ailment; //XOR�� ���ְ�
        EndOFAilmentEvent?.Invoke(ailment); //���� �˸�.
    }
    private void AilmentDamage()
    {
        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if ((currentAilment & ailment) > 0)
            {
                Debug.Log(currentAilment);
            }
        }
    }
    public void UsedToAilment(AilmentEnum ailment)
    {
        if (HasAilment(ailment))
        {
            switch (ailment)
            {
                case AilmentEnum.None:
                    break;
                case AilmentEnum.Chilled:

                    break;
                case AilmentEnum.Shocked:
                    break;
            }
        }
    }


    //Ư�� ������� �����ϴ��� üũ
    public bool HasAilment(AilmentEnum ailment)
    {
        return (currentAilment & ailment) > 0;
    }
    public int GetStackAilment(AilmentEnum ailment)
    {
        return _ailmentDictionary[ailment].stack;
    }

    public void ApplyAilments(AilmentEnum value, int turn)
    {
        Debug.Log(value);
        currentAilment |= value; //���� �����̻� �߰� �����̻� �������

        //�����̻� ���� ���� �ֵ��� �ð� �������ְ�. 
        if ((value & AilmentEnum.Chilled) > 0)
        {
            SetAilment(AilmentEnum.Chilled, turn);
        }
        else if ((value & AilmentEnum.Shocked) > 0)
        {
            SetAilment(AilmentEnum.Shocked, turn);
        }
    }

    //����ȿ���� ���ӽð� ����
    private void SetAilment(AilmentEnum ailment, int turn)
    {
    }

}