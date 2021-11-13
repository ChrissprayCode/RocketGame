using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float upwardsThrust = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    AudioSource audioSource;
    [SerializeField] AudioClip Engine;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem mainThrustParticle;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * upwardsThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(Engine, 0.5f);
            }

            if (!mainThrustParticle.isPlaying)
            {
                mainThrustParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainThrustParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
            if (!rightThrustParticle.isPlaying)
            {
                rightThrustParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
            if (!leftThrustParticle.isPlaying)
            {
                leftThrustParticle.Play();
            }
        }
        else
        {
            rightThrustParticle.Stop();
            leftThrustParticle.Stop();
        }
    }
}
