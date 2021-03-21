using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float mouseSensitivity = 3f;

    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) transform.position += transform.forward * playerSpeed * Time.deltaTime; 
        if (Input.GetKey("s")) transform.position -= transform.forward * playerSpeed * Time.deltaTime;
        if (Input.GetKey("d")) transform.position += transform.right * playerSpeed * Time.deltaTime;
        if (Input.GetKey("a")) transform.position -= transform.right * playerSpeed * Time.deltaTime;

        transform.Rotate(0,Input.GetAxis("Mouse X")*mouseSensitivity,0,Space.World);
        cam.Rotate(-Input.GetAxis("Mouse Y")*mouseSensitivity,0,0,Space.Self);
    }
}
