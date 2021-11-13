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
    [SerializeField] ParticleSystem fuelParticle;
    float maxFuel = 100f;
    [SerializeField] float fuel = 100f;
    [SerializeField] float mainThrustCost = 0.03f;
    [SerializeField] float sideThrustCost = 0.01f;



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
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            Debug.Log(fuel);
            fuel -= mainThrustCost;
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
        if (fuel > 0)
        {
            
            if (Input.GetKey(KeyCode.A))
            {
                fuel -= sideThrustCost;
                Debug.Log(fuel);
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
                fuel -= sideThrustCost;
                Debug.Log(fuel);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fuel")
        {
            Destroy(other.gameObject);
            fuelParticle.Play();
            AddFuel();
        }
    }

    void AddFuel()
    {
        fuel += 50;
        if(fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }
}
