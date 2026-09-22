using System;
using UnityEngine;
using TMPro;

public class PinReset: MonoBehaviour
{
    public Pin[] pins;
    public TextMeshProUGUI scoreText;
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
           Debug.Log(startPositions[i]);
        }
    }

    public void resetPins()
    {
        int fallencount = 0;
        foreach (var pin in pins)
        {
            if (pin.isFallen())
                fallencount++;
        }
        if (scoreText)
            scoreText.text = "Score: " + fallencount;
        
        for (int i = 0; i < pins.Length; i++)
        {
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            
            rb.linearVelocity  = Vector3.zero;
            rb.angularVelocity  = Vector3.zero;
            
            rb.position = startPositions[i];
            rb.rotation = startRotations[i];
        }
    }
}