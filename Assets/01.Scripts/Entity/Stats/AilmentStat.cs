using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentStat
{
    private Health _health;

    private Dictionary<AilmentEnum, int> _ailmentTurn;
    private Dictionary<AilmentEnum, int> _ailmentStack;

    public AilmentEnum currentAilment; //���� �� ����� ����

    public event Action<AilmentEnum> EndOFAilmentEvent; // �����̻� ����� �߻�

    public AilmentStat(Health health)
    {
        _ailmentTurn = new Dictionary<AilmentEnum, int>();
        _ailmentStack = new Dictionary<AilmentEnum, int>();

        _health = health;

        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (ailment == AilmentEnum.None) continue;
            _ailmentTurn.Add(ailment, 0);
            _ailmentStack.Add(ailment, 0);
        }
    }

    public void UpdateAilment()
    {
        AilmentDamage();
        foreach (AilmentEnum ailment in Enum.GetValues(typeof(AilmentEnum)))
        {
            if (ailment == AilmentEnum.None) continue;

            if (HasAilment(ailment))
                _ailmentTurn[ailment]--;

            if (_ailmentTurn[ailment] <= 0)
            {
                CuredAilment(ailment);
            }
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
            if (HasAilment(ailment))
            {
                switch (ailment)
                {
                    default:
                        break;
                }
            }
        }
    }
    public void UsedToAilment(AilmentEnum ailment)
    {
        if (!HasAilment(ailment))
            return;


        switch (ailment)
        {
            case AilmentEnum.None:
                break;
            case AilmentEnum.Chilled:
                break;
            case AilmentEnum.Shocked:
                _ailmentStack[ailment] = 0;
                _health.AilementDamage(ailment, 2);
                break;
        }
    }


    //Ư�� ������� �����ϴ��� üũ
    public bool HasAilment(AilmentEnum ailment)
    {
        bool temp = ((currentAilment & ailment) > 0);
        return (currentAilment & ailment) > 0;
    }

    public int GetStackAilment(AilmentEnum ailment)
    {
        return _ailmentStack[ailment];
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
        _ailmentTurn[ailment] = turn;
    }
}