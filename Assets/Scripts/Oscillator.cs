using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    const float tau = Mathf.PI * 2; //6.283 ) how many radians in a circle
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; } //get rid of NaN errors (Don't run when it's trying to divide by 0(or VERY close to 0))

        float cycles = Time.time / period; //continually grows over time
        float rawSinWave = Mathf.Sin(cycles * tau); //Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //Changing the wave to go from 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
