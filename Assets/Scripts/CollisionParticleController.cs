using System;
using UnityEngine;

public class CollisionParticleController : MonoBehaviour
{
    private Action onParticleSystemStopped;
    private ParticleSystem particle;

    private void Start()
    {
        // In order to receive the callback, you must set the ParticleSystem.MainModule.stopAction property to Callback. (Unity Doc)
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;

        particle = GetComponent<ParticleSystem>();
    }

    public void Play(Action onParticleSystemStopped = null)
    {
        this.onParticleSystemStopped = onParticleSystemStopped;

        this.particle = GetComponent<ParticleSystem>();
        this.particle.Play();
    }

    private void OnParticleSystemStopped()
    {
        this.particle.Stop();
        this.onParticleSystemStopped?.Invoke();
    }
}
