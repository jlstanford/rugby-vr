
using System;
// using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winger :  Player
{

    private float throwVal = 3f;
    private float moveVal = 30;
    // private GameObject ball;
    

    // Start is called before the first frame update

    void Start()
    {
        // simpleCharacterController.playerSpeed = speedMod;
        playerState = PlayerState.STAGGERED;
        actionsCmps = GetComponentsInChildren<VRGrab>();
        simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
        foreach(VRGrab actionsCmp in actionsCmps) // for all objects using VRGrab
        {
            actionsCmp.throwForce = throwVal;
        }
        simpleCharacterController.moveSpeed = moveVal;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        base.init();
        // Debug.Log(playerTeam);
        Debug.Log(playerTeam);
        findBallInGame(playerTeam.game);
        Debug.Log("Winger "+ball);
        
        // findBallInGame(playerTeam.game);
        // Debug.Log("Winger "+ball);
        Debug.Log(ball.GetComponent<Ball>());
        //ball is the GameObject, getComoonent gets the Ball script
        isBallHolder = ball.GetComponent<Ball>().isBeingHeld;
        // isBallHolder = true;
        
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
            playerState = PlayerState.STAGGERED;
        } else if(!isBallHolder && playerTeam.teamState == TeamScript.TeamState.DEF)
        {
            playerState = PlayerState.FLAT;
        }
    }

    public void scoreTry()
    {
        // playerTeam.updateScore( playerTeam.getScore() + 5 );
    }

    public void pass(Ball ball)
    {

    }

    public void grab(Ball ball)
    {

    }

}
