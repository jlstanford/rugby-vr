using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIsOut : BallState
{
    public BallState DoState(Ball ball)
    {
        ball.isOut = true;
        ball.isBeingHeld = false;
        ball.isBeingPassed = false;
        ball.transform.SetParent(null);
        Debug.Log("Ball is out");
        ball.GetComponent<CapsuleCollider>().isTrigger = true;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Rigidbody>().useGravity = false;
        return Ball.ballIsOut;
    }
}