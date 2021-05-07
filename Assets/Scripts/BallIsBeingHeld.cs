using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIsBeingHeld : BallState
{
    public BallState DoState(Ball ball)
    {
         if(ball.transform.position.y <= 0 && ball.isBeingHeld == false && ball.GetComponentInParent(typeof(Player)) == null)
        {
            return Ball.ballIsOut;
        }
        // ball.GetComponent<Transform>().position = this.position;
        ball.GetComponent<Rigidbody>().useGravity = false;
        // game.ball.transform.SetParent(this.transform);
        ball.GetComponent<Rigidbody>().isKinematic = true;
        // heldObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Ball>().isBeingPassed = false;
        ball.GetComponent<Ball>().isBeingHeld = true;
        ball.GetComponent<Ball>().isOut = false;
        return Ball.ballIsBeingHeld;
    }
}