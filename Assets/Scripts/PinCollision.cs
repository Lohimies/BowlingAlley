using System;
using UnityEngine;

public class PinCollision : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 0.1f;
    public float minImpact = 0.3f;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("pin collision");
        float impact = collision.relativeVelocity.magnitude;

        if (impact < minImpact)
            return;
        
        float volume = Mathf.Clamp01(impact * maxVolume);
        audioSource.volume = volume;
        audioSource.Play();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}

