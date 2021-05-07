using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : Player
{
    void Start(){
        actionsCmps = GetComponentsInChildren<VRGrab>();
        simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
        currentState = PlayerManager.linedUp; 
    }
    void Update(){
        foreach(VRGrab handCmp in actionsCmps)
        {
            if(handCmp.heldObject != null) this.heldObject = handCmp.heldObject;
        }
        this.side = playerManager.sides[playerTeam];
        this.tryZone = playerManager.tryZoneAreas[side];
        nearestEnemy = getNearestEnemy();
        nearestTeammate = getNearestTeammate();
        // Debug.Log("distance for player" +this+" is:"+playerManager.getDistanceFromNearestEnemy(this));
        distanceFromNearestEnemy = playerManager.getDistanceFromNearestEnemy(this); 
        previousStateName = currentState.ToString();
        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();
    }

    
    public override Player getNearestEnemy()
    {
        return playerManager.getNearestEnemyFor(this);
    }

    public override Player getNearestTeammate()
    {
        return playerManager.getNearestTeammateFor(this);

    }

    

    public override void passTo(Player targetPlayer)
    {
        ball.transform.SetParent(null);
        var point = targetPlayer.transform.position;
        Debug.Log("Passing to " + targetPlayer );
        // var velocity = calculatePassVelocity(point, 45);
        // velocity = velocity * throwForce;
        ball.GetComponent<Ball>().BePassedBy(this,point);
        isBallHolder = false;
        heldObject = null;
        
    }

    public Vector3 calculatePassVelocity(Vector3 recievingPlayer, float angle)
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

    public override void catch_(GameObject ball)
    {
        heldObject = ball;
        Debug.Log(this+"Caught the Ball!"+ball);
        ball.transform.SetParent(this.transform);
        // ball.transform.position.y = 1.0f;
        ball.transform.position = new Vector3(ball.transform.position.x,1.0f,ball.transform.position.z);
        // heldObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Ball>().isBeingPassed = false;
        ball.GetComponent<Ball>().isBeingHeld = true;
        ball.GetComponent<Ball>().isOut = false;
        ball.GetComponent<Ball>().currentBallState = Ball.ballIsBeingHeld;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        isBallHolder = true;
        // playerManager.updatePossession(playerTeam);
        playerManager.ballHolder = this;
        heldObject = ball;
    }
    public override void pickUp(GameObject ball)
    {
        heldObject = ball;
        Debug.Log("Picked up the Ball!");
        isBallHolder = true;
        heldObject = ball;
        playerManager.ballHolder = this;
        playerManager.updatePossession(playerTeam);
        ball.GetComponent<Ball>().bePickedUpBy(this);
    }
    public override void lineUp()
    {
        Debug.Log(this+"calling goToStartPosition");
        goToStartPosition();
    }
    public override void tackle(Player targetPlayer)
    {
        // targetPlayer.playerState = PlayerState.DOWN;
        // targetPlayer.drop(ball.GetComponent<Ball>());
        base.tackle(targetPlayer);
    }

    public override void getStaggered()
    {
        Debug.Log("VR Player get staggered");
    }
     
    
}