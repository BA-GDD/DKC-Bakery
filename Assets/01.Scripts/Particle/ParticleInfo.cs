using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Particle.Trigger;
using System;

namespace Particle
{
    public class ParticleInfo : MonoBehaviour
    {
        private ParticleSystem ps;

        [SerializeField]private List<ParticleTriggerInfo> triggerInfos;
        //[SerializeField] private TakeDamageParticle[] takeDamages;

        public float duration;

        private List<Health> _targets = new();
        public int[] damages { get; set; }
        public Entity owner { get; set; }

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }
        private void Start()
        {
            foreach (ParticleTriggerInfo i in triggerInfos)
            {
                i.Owner = owner;
                i.Targets = _targets;
                i.Damages = damages;
            }
        }

        public void OnEnable()
        {
            _targets.Clear();
        }

        public void SetTriggerTarget(Entity target)
        {
            foreach (var col in triggerInfos)
            {
                col.AddCollision(target.ColliderCompo);
            }
            _targets.Add(target.HealthCompo);
        }

        public void StartParticle(Action OnStartParticleEvent, Action OnEndParticleEvent)
        {
            ps.Play();
            OnStartParticleEvent?.Invoke();
            StartCoroutine(WaitEndParticle(OnEndParticleEvent));
        }
        public void EndParticle(Action OnEndParticleEvent)
        {
            ps.Stop();
            OnEndParticleEvent?.Invoke();
        }
        private IEnumerator WaitEndParticle(Action OnEndParticleEvent)
        {
            yield return new WaitForSeconds(duration);
            EndParticle(OnEndParticleEvent);
        }
    }
}
