

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AilmentStat
{
    private Dictionary<Ailment, int> _ailmentTimerDictionary;
    private Dictionary<Ailment, int> _ailmentDamageDictionary;
    private Dictionary<Ailment, int> _ailmentStackDictionary;


    public Ailment currentAilment; //질병 및 디버프 상태

    public event Action<Ailment, int> AilmentDamageEvent; //상태이상 종류와 데미지 이벤트
    public event Action<Ailment> EndOFAilmentEvent; // 상태이상 종료시 발생

    private float _igniteTimer;
    private float _igniteDamageCooldown = 0.3f;

    public AilmentStat()
    {
        _ailmentTimerDictionary = new Dictionary<Ailment, int>();
        _ailmentDamageDictionary = new Dictionary<Ailment, int>();
        _ailmentStackDictionary = new Dictionary<Ailment, int>();

        foreach (Ailment ailment in Enum.GetValues(typeof(Ailment)))
        {
            if (ailment != Ailment.None)
            {
                _ailmentTimerDictionary.Add(ailment, 0);
                _ailmentDamageDictionary.Add(ailment, 0); //데미지와 쿨타임 초기화
                _ailmentStackDictionary.Add(ailment, 0);
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
                _ailmentTimerDictionary[ailment]--;
                if (_ailmentTimerDictionary[ailment] <= 0)
                {
                    currentAilment ^= ailment; //XOR로 빼주고
                    EndOFAilmentEvent?.Invoke(ailment); //종료 알림.
                }
            }
        }

        //DOT 데미지들은 여기서 처리.
        IgniteTimer();
    }

    private void IgniteTimer() //점화의 경우 틱데미지를 줘야 하니까.
    {
        if ((currentAilment & Ailment.Ignited) == 0) return;

        _igniteTimer += Time.deltaTime;
        if (_ailmentTimerDictionary[Ailment.Ignited] > 0 && _igniteTimer > _igniteDamageCooldown)
        {
            _igniteTimer = 0;
            AilmentDamageEvent?.Invoke(Ailment.Ignited, _ailmentDamageDictionary[Ailment.Ignited]);
        }
    }

    //특정 디버프가 존재하는지 체크
    public bool HasAilment(Ailment ailment)
    {
        return (currentAilment & ailment) > 0;
    }

    public void ApplyAilments(Ailment value, int turn, int damage)
    {
        currentAilment |= value; //현재 상태이상에 추가 상태이상 덧씌우고

        //상태이상 새로 들어온 애들은 시간 갱신해주고. 
        if ((value & Ailment.Ignited) > 0)
        {
            SetAilment(Ailment.Ignited, turn, damage);
        }
        else if ((value & Ailment.Chilled) > 0)
        {
            SetAilment(Ailment.Chilled, turn, damage);
        }
        else if ((value & Ailment.Shocked) > 0)
        {
            SetAilment(Ailment.Shocked, turn, damage);
        }
    }

    //질병효과와 지속시간 셋팅
    private void SetAilment(Ailment ailment, int turn, int damage)
    {
        _ailmentTimerDictionary[ailment] = turn;
        _ailmentDamageDictionary[ailment] = damage;
    }

}