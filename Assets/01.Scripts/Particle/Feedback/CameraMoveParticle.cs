using Particle.Trigger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveParticle : ParticleTriggerEventBase
{
    private PoolVCam cam;
    public override void Action(ref ParticleSystem.Particle p)
    {
        CameraController.Instance.SetTransitionTime(0.1f);
        if (cam == null)
        {
            cam = CameraController.Instance.GetVCam();
            return;
        }
        Vector3 backwardPos = transform.TransformPoint(p.position);
        backwardPos.z = -10;
        cam.transform.position = backwardPos;
    }
}
