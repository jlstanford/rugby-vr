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
        base.init();
        playerState = PlayerState.STAGGERED;
        actionsCmps = GetComponentsInChildren<VRGrab>();
        simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
        // foreach(VRGrab actionsCmp in actionsCmps) 
        // {
        //     actionsCmp.throwForce = throwVal;
        // }
        // if(simpleCharacterController != null)
        // {
        //     simpleCharacterController.moveSpeed = moveVal;
        // }
    } 

    void update()
    {
        
        Debug.Log("Hooker "+playerTeam);
        findBallInGame(playerTeam.game);
        isBallHolder = ball.GetComponent<Ball>().isBeingHeld;
        if(isBallHolder)
        {
            playerState = PlayerState.BALL_CARRYING;
            playerTeam.teamState = TeamScript.TeamState.OFF; 
            //If player is tackled
            // playerState = PlayerState.DOWN;

            //If player dodges/performs step
            // playerState = playerState;

            //If player passes
            // playerState = PlayerState.STAGGERED;
        } else if(!isBallHolder && playerTeam.teamState == TeamScript.TeamState.OFF) { 
            Debug.Log(playerTeam);
            Debug.Log(playerTeam.teamState);
            Debug.Log(playerTeam.teamState == TeamScript.TeamState.OFF);
            playerState = PlayerState.STAGGERED;
        } else if(!isBallHolder && playerTeam.teamState == TeamScript.TeamState.DEF)
        {
            Debug.Log(playerTeam);
            Debug.Log(playerTeam.teamState);
            Debug.Log(playerTeam.teamState == TeamScript.TeamState.DEF);
            playerState = PlayerState.FLAT;
        }
    }
}