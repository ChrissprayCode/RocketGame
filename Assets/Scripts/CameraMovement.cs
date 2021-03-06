using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 camOffset = new Vector3(0,0,-10);
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = player.transform.position + camOffset;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + camOffset;
    }
}
