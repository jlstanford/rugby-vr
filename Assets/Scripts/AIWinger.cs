using System;
// using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWinger :  AIPlayer
{
 
    private float throwVal = 3f;
    private float moveVal = 30;
    private float homeXOffset = 15;
    private float homeZOffset = 40;
    void Start() 
    {
        throwForce = throwVal;
        moveSpeed = moveVal;
        Debug.Log("player side "+side);
       if(side == Game.Side.NORTH)
        {
            if(playerNumber == 11){
                Debug.Log("player 11 north: "+this);
                homePosition = new Vector3(game.middle.x+homeXOffset,0,game.middle.z+homeZOffset);
            }else if(playerNumber == 14){
                Debug.Log("player 14 north: "+this);
                homePosition = new Vector3(game.middle.x-homeXOffset,0,game.middle.z+homeZOffset);
            }
        }else if(side == Game.Side.SOUTH)
        {
            if(playerNumber == 11){
                
                Debug.Log("player 11 south: "+this);
                homePosition = new Vector3(game.middle.x+homeXOffset,0,game.middle.z-homeZOffset);
            }else if(playerNumber == 14){
                
                Debug.Log("player 14 south: "+this);
                homePosition = new Vector3(50-homeXOffset,0,50-homeZOffset);
            }
        }
        
        
    } 

 


}
