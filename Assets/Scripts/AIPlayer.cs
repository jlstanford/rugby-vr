using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : Player
{
    // public Player nearestEnemy;
    public Player passReciever;
    void start(){
        actionsCmps = GetComponents<AIActions>();
        Debug.Log("AIPLAYER ACTIONS: "+actionsCmps);
        simpleCharacterController = GetComponent<AIMovementController>();
        
    }
    void update(){
        // nearestEnemy = getNearestEnemy();
        // Debug.Log("AIPlayer update");
        // Debug.Log("AIPlayer "+actionsCmps);
        // foreach(AIActions actionScript in actionsCmps)
        // {
        //     if(actionScript.heldObject != null) this.heldObject = actionScript.heldObject;
        // }
    }

    

    public virtual void passTo(Player targetPlayer)
    {
        var point = targetPlayer.transform.position;
        var velocity = calculatePassVelocity(point, 45);
        velocity = velocity * throwForce;
        Debug.Log("Firing at " + point + " velocity " + velocity);

        ball.GetComponent<Rigidbody>().transform.position = transform.position;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        ball.GetComponent<Ball>().isBeingPassed = true;
        ball.GetComponent<Ball>().isBeingHeld = false;
        ball.transform.SetParent(GetComponent<AIPlayer>().game.transform); 
        // this.ball = null;
        isBallHolder = false;
        heldObject = null;
        
    }

    private Vector3 calculatePassVelocity(Vector3 recievingPlayer, float angle)
    {
        Vector3 dir = recievingPlayer - this.position; // get Target Direction
            float height = dir.y; // get height difference
            dir.y = 0; // retain only the horizontal difference
            float dist = dir.magnitude; // get horizontal distance
            float a = angle * Mathf.Deg2Rad; // Convert angle to radians
            dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
            dist += height / Mathf.Tan(a); // Correction for small height differences

            // Calculate the velocity magnitude
            float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
            return velocity * dir.normalized; // Return a normalized vector.
    }

    public virtual void catch_(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        heldObject = ball;
        Debug.Log("Caught the Ball!");
        ball.transform.SetParent(this.transform);
        ball.GetComponent<Transform>().position = position;
        ball.GetComponent<Transform>().position = this.position;
        game.ball.transform.SetParent(this.transform);
        // heldObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Ball>().isBeingPassed = false;
        ball.GetComponent<Ball>().isBeingHeld = true;
    }

}