using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker :  Player
{
    
    private float throwVal = 1.5f;
    private float moveVal = 15;
    void Start()
    {
        // simpleCharacterController.playerSpeed = speedMod;
        actionsCmps = GetComponentsInChildren<VRGrab>();
        simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
        foreach(VRGrab actionsCmp in actionsCmps) 
        {
            actionsCmp.throwForce = throwVal;
        }
        simpleCharacterController.moveSpeed = moveVal;
    }

    void update()
    {
        isBallHolder = ball.GetComponent<Ball>().isBeingHeld;
    }
}