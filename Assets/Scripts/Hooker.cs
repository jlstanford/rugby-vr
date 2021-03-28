using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker :  Player
{
    
    private float throwVal = 1.5f;
    private float moveVal = 15;
    void Start() 
    {
        throwForce = throwVal;
        moveSpeed = moveVal;
    } 

    void update()
    {
        
    //set isbBallHolder
    // heldObject = actionsCmps[0].heldObject;  
    // isBallHolder = heldObject.GetComponent<Ball>().isBeingHeld;
    // updatePossession(playerTeam);
    }
}