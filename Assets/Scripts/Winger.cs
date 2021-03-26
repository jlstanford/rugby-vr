using System;
// using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winger :  Player
{
 
    private float throwVal = 3f;
    private float moveVal = 30;
    void Start() 
    {
 
    } 

    void update()
    {
        
    //set isbBallHolder
    // heldObject = actionsCmps[0].heldObject;  
    // isBallHolder = heldObject.GetComponent<Ball>().isBeingHeld;
    // updatePossession(playerTeam);
        if(actionsCmps[0].heldObject)
        {
        heldObject = actionsCmps[0].heldObject;  //TODO: make it check left (actioncmps[0]) and right (actioncmps[1]) hands
        isBallHolder = heldObject.GetComponent<Ball>().isBeingHeld;
        updatePossession(playerTeam); 
        }
    }
}
