using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIsBeingKickedOff : BallState
{
    public BallState DoState(Ball ball)
    {
        ball.isOut = false;
        ball.isBeingHeld = false;
        ball.isBeingPassed = true;
        // when ball stops moving
        //  return ball.ballIsOut;
        if(ball.GetComponent<Rigidbody>().velocity.magnitude > 1 ) // ball moving faster than 1m/s
        {
            return Ball.ballIsBeingKickedOff;
        }else
        {
            return Ball.ballIsOut;
        }
        
    }
}