using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BallRoll : MonoBehaviour
{
    [Header("Ground Detection")]
    [SerializeField] private float groundCheck = 0.1f;
    [SerializeField] private LayerMask floor;
    
    public Transform respawnPoint;
    public PinReset pinResetter;
    
    
    [Header("Sound Settings")]
    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float volumeScale = 0.5f;
    
    private AudioSource audioSource;
    private Rigidbody rb;

    public bool IsGrounded()
    { 
        return Physics.Raycast(transform.position,Vector3.down, groundCheck, floor);
    }
    
    private void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        rb.position = respawnPoint.position;
        rb.rotation = respawnPoint.rotation;

        if (pinResetter)
            pinResetter.resetPins();
    }
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        bool isMoving  = rb.linearVelocity.magnitude > moveThreshold;
        
        if (IsGrounded() && isMoving)
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
        
        if (IsGrounded() && rb.linearVelocity.magnitude < moveThreshold)
        {
            Debug.Log("ball respawn");
            Respawn();
            
        }
    }
}
