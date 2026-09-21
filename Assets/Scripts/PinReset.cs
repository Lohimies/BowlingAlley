using System;
using UnityEngine;
public class PinReset: MonoBehaviour
{
    public Pin[] pins;
    private Vector3[] startPositions;
    private Quaternion[]  startRotations;

    private void Start()
    {
        startPositions = new Vector3[pins.Length];
        startRotations = new Quaternion[pins.Length];

        for (int i = 0; i < pins.Length; i++)
        {
           startPositions[i] = pins[i].transform.position;
           startRotations[i] = pins[i].transform.rotation;
        }
    }

    private void checkPins()
    {
        foreach (var pin in pins)
        {
            if (!pin.isFallen())
                return;
        }

        resetPins();
    }

    private void resetPins()
    {
        for (int i = 0; i < pins.Length; i++)
        {
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            
            rb.linearVelocity  = Vector3.zero;
            rb.angularVelocity  = Vector3.zero;
            
            rb.position = startPositions[i];
            rb.rotation = startRotations[i];
        }
    }

    private void FixedUpdate()
    {
        checkPins();
    }
}