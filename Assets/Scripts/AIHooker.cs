using System;
using System.Collections;
using UnityEngine;

public class AIHooker :  AIPlayer
{
 
    private float throwVal = 1.5f;
    private float moveVal = 15;
    private float homeXOffset = 0;
    private float homeZOffset = 5;
    void Start() 
    {
        throwForce = throwVal;
        moveSpeed = moveVal;
        if(side == Game.Side.NORTH)
        {
            homePosition = new Vector3(game.middle.x+homeXOffset,0,game.middle.z+homeZOffset);    
        }else if(side == Game.Side.SOUTH)
        {
            homePosition = new Vector3(game.middle.x+homeXOffset,0,game.middle.z-homeZOffset);    
        }
        // homePosition = new Vector3(50,0,50);
    } 

 


}
