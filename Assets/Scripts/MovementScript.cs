using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Transform location;
    public Vector3 position;
    public float moveSpeed;
    public float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
           
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rotate left
            transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rotate right
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed, Space.Self);
        }

    }
}
