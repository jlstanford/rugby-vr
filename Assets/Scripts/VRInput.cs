using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInput : MonoBehaviour
{
    public Hands hand = Hands.Left;
    public float gripValue;
    public float joystickAxisHorizontalValue;
    public float joystickAxisVerticalValue;

    public Vector3 velocity;
    public Vector3 angularVelocity;

    private Vector3 previousPosition;
    private Vector3 previousAngularRotation;

    private string gripAxis;
    private string joystickAxisVertical;
    private string joystickAxisHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        gripAxis = $"{hand}Grip";  
        joystickAxisVertical = $"XRI_{hand}_Primary2DAxis_Vertical";  
        joystickAxisHorizontal = $"XRI_{hand}_Primary2DAxis_Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        gripValue = Input.GetAxis(gripAxis);
        joystickAxisVerticalValue = Input.GetAxis(joystickAxisVertical);
        joystickAxisHorizontalValue = Input.GetAxis(joystickAxisHorizontal);

        // controller velocity
        velocity = (this.transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
        angularVelocity = (transform.eulerAngles - previousAngularRotation) / Time.deltaTime;
    }

    [System.Serializable]
    public enum Hands 
    { 
        Left,
        Right
    }
}
