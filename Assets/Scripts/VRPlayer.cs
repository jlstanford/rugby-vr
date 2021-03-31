using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : Player
{
    void start(){
        actionsCmps = GetComponentsInChildren<VRGrab>();
        simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
    }
    void update(){
        foreach(VRGrab handCmp in actionsCmps)
        {
            if(handCmp.heldObject != null) this.heldObject = handCmp.heldObject;
        }
    }
}