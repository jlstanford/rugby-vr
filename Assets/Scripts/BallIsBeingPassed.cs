using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIsBeingPassed : BallState
{
    public BallState DoState(Ball ball)
    {
        ball.isOut = false;
        ball.isBeingHeld = false;
        ball.isBeingPassed = true;
        ball.GetComponent<CapsuleCollider>().isTrigger = false;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.GetComponent<Rigidbody>().useGravity = true;
        // when ball stops moving
        //  return ball.ballIsOut;
        if(ball.GetComponent<Rigidbody>().velocity.magnitude > 1 )
        {
            return Ball.ballIsBeingPassed;
        }else
        {
            return Ball.ballIsOut;
        }
    }
}