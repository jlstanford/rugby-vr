using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIsReady : BallState
{
    public BallState DoState(Ball ball)
    {
        // ball.reset();
        // ball.GetComponent<CapsuleCollider>().isTrigger = true;
        // ball.GetComponent<Rigidbody>().isKinematic = true;
        // ball.GetComponent<Rigidbody>().useGravity = false;
        if(ball.game.currentGameState == Game.GameState.KICKOFF )
        {
            return Ball.ballIsBeingKickedOff;
        }
        else if(ball.game.currentGameState == Game.GameState.PLAYING )
        {
            return Ball.ballIsOut;
        }
        else
        {
            return Ball.ballIsReady;
        }
        
    }
}