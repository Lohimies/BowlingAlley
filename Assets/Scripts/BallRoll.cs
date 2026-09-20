using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BallRoll : MonoBehaviour
{
    [Header("Ground Detection")]
    [SerializeField] private float groundCheck = 0.1f;
    [SerializeField] private LayerMask floor;
    
    
    [Header("Sound Settings")]
    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float volumeScale = 0.5f;
    
    private AudioSource audioSource;
    private Rigidbody rb;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        bool isGrounded = Physics.Raycast(transform.position,Vector3.down, groundCheck, floor);
        bool isMoving  = rb.linearVelocity.magnitude > moveThreshold;
        
        if (isGrounded && isMoving)
        {
            if (!audioSource.isPlaying) audioSource.Play();

            float targetVolume = Mathf.Clamp01(rb.linearVelocity.magnitude * volumeScale);
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.fixedDeltaTime * 10f);
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            audioSource.volume = 0f;
        }
    }
}
