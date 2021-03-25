using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRJoystickMovement : MonoBehaviour
{
    private VRInput controller;
    private Camera playerCam;
    public Vector3 position;
    public Transform location;
    public float moveSpeed;
    // public float turnSpeed;

    void Awake()
    {
        controller = GetComponentInChildren<VRInput>();
        playerCam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (!controller)
        {
            return;
        }
        // Debug.Log(controller);
        if (controller.joystickAxisVerticalValue < 0)
        {
            Debug.Log("Going Forward");
            // transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            float playerY = transform.position.y;
            transform.Translate(playerCam.transform.forward * Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, playerY, transform.position.z); //comment out if you wanna FLYYY ;)
        }
        if (controller.joystickAxisVerticalValue > 0)
        {
            Debug.Log("Going Backward");
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
            // transform.Translate(-playerCam.transform.forward * Time.deltaTime * moveSpeed);
        }
        if (controller.joystickAxisHorizontalValue < 0)
        {
            Debug.Log("Going left");
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (controller.joystickAxisHorizontalValue > 0)
        {
            Debug.Log("Going Right");
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

    }


}