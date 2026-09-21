using UnityEngine;

public class BallReset : MonoBehaviour
{
    public Rigidbody rb;
    public Transform respawnPoint;
    public float minSpeedToRespawn = 0.3f;

    private bool canReset;

    private void OnCollisionEnter(Collision collision)
    {
        canReset = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("can reset");
        canReset = true;
    }

    private void FixedUpdate()
    {
        if (canReset && rb.linearVelocity.magnitude < minSpeedToRespawn)
        {
            Debug.Log("ball respawn");
            Respawn();
        }
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        canReset = false;
        
        rb.position = respawnPoint.position;
        rb.rotation = respawnPoint.rotation;
    }
}
