using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform location;
    public Vector3 position;
    public float moveSpeed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        position = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
       // position.y = 0f;
      //  position.x = 0f;
        position.y = Input.GetAxis("Mouse Y");
       // position.x = Input.GetAxis("Mouse X");
        //position.Normalize();
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,Vector3.up);
        //float smoothing = 5f;
       // transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, Time.deltaTime * smoothing);
     
       // #region Rotation using Mouse
     //   transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * turnSpeed, Space.Self);
       // transform.Rotate(Vector3.down * Input.GetAxis("Mouse Y") * turnSpeed, Space.Self);
       // #endregion
    }
}
