using System;
using UnityEngine;

public class PinCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 0.1f;
    public float minImpact = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (impact < minImpact)
            return;
        
        float volume = Mathf.Clamp01(impact * maxVolume);
        audioSource.volume = volume;
        audioSource.Play();
    }
}

