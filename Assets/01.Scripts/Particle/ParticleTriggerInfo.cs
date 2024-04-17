using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


namespace Particle.Trigger
{
    [Flags]
    public enum ParticleTriggerType
    {
        None = 0,
        Inside = 1,
        Outside = 2,
        Enter = 4,
        Exit = 8
    }
    public class ParticleTriggerInfo : MonoBehaviour
    {
        public delegate void ParticleTriggerEvent(ref ParticleSystem.Particle p);

        private ParticleSystem ps;
        private ParticleSystem.TriggerModule triggerModule;

        //enum 순서에 따라 맞는 이벤트
        public ParticleTriggerEvent[] triggerEvent = new ParticleTriggerEvent[4];
        public void AddEvent(ParticleTriggerEventBase b)
        {
            ParticleTriggerType copyType = b.Type;
            for (int i = 0; i < 4; i++)
            {
                if (((1 << i) & (int)copyType) > 0)
                {
                    triggerEvent[i] += b.Action;
                }
            }
        }

        public Entity Owner { get; set; }
        public List<Health> Targets { get; set; }
        public int[] Damages { get; set; }

        public void AddCollision(Collider2D col) => triggerModule.AddCollider(col);
        public void RemoveCollision(Collider col) => triggerModule.RemoveCollider(col);
        public void ClearCollision()
        {
            for (int i = 0; i < triggerModule.colliderCount; i++)
            {
                triggerModule.RemoveCollider(i);
            }
        }

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
            triggerModule = ps.trigger;

            ParticleTriggerEventBase[] events = GetComponents<ParticleTriggerEventBase>();
            foreach (var e in events)
            {
                e.Init(this);
                AddEvent(e);
            }
        }


        private void OnParticleTrigger()
        {
            foreach (ParticleSystemTriggerEventType type in Enum.GetValues(typeof(ParticleSystemTriggerEventType)))
            {
                if (!IsCallEventType(type)) continue;
                List<ParticleSystem.Particle> particleList = new();
                int chk = ps.GetTriggerParticles(type, particleList);
                for (int i = 0; i < chk; i++)
                {
                    ParticleSystem.Particle p = particleList[i];
                    triggerEvent[(int)type]?.Invoke(ref p);
                    particleList[i] = p;
                }
                ps.SetTriggerParticles(type, particleList);
            }
        }
        private bool IsCallEventType(ParticleSystemTriggerEventType type)
        {
            switch (type)
            {
                case ParticleSystemTriggerEventType.Inside:
                    return triggerModule.inside == ParticleSystemOverlapAction.Callback;
                case ParticleSystemTriggerEventType.Outside:
                    return triggerModule.outside == ParticleSystemOverlapAction.Callback;
                case ParticleSystemTriggerEventType.Enter:
                    return triggerModule.enter == ParticleSystemOverlapAction.Callback;
                case ParticleSystemTriggerEventType.Exit:
                    return triggerModule.exit == ParticleSystemOverlapAction.Callback;
                default:
                    return false;
            }
        }
    }
}

