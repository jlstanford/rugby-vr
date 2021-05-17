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
        isBallHolder = true;
        heldObject = ball;
        playerManager.ballHolder = this;
        playerManager.updatePossession(playerTeam);
        if(ball != null && ball.TryGetComponent(out Ball ballObj))
        {
            Debug.Log("VR Picked up the Ball!");
            ballObj.GetComponent<Ball>().bePickedUpBy(this);
        }
        
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

     public void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("trigger override");
        collidingObject = other.gameObject;
        
        Debug.Log("player performing trigger action"+GetComponent<VRPlayer>());
        Debug.Log("collding object tag:"+collidingObject.tag);
        Debug.Log("ballHolding player:"+GetComponent<VRPlayer>().isBallHolder);

        if(collidingObject.tag == $"{playerTeam.getOpposingTeam()} Player" && isBallHolder == true ) {
            Debug.Log("in go down trigger");
            currentState = PlayerManager.goingDown;
        }else if( collidingObject.GetComponent(typeof(Player)) == playerManager.ballHolder && playerManager.ballHolder != null ) { // collidingObject.TryGetComponent<AIPlayer>(out AIPlayer collidingPlayer) == true
            // collidingObject.GetComponent<AIPlayer>().playerTeam != this.playerTeam
            Debug.Log("tackling player - "+collidingObject.GetComponent(typeof(Player)));
            // Debug.Log("tackling playerTeam - "+collidingObject.GetComponent(typeof(Player)).playerTeam);
            // tackle(collidingObject.GetComponent<AIPlayer>());
            base.actionTarget = (Player)collidingObject.GetComponent(typeof(Player));
            base.collidingObject = collidingObject;
            currentState = PlayerManager.tackling;
        }else if(collidingObject.tag == "GameBall" && collidingObject.GetComponent<Ball>().isBeingPassed && collidingObject.GetComponent<Ball>().isOut == false) {
            // attemptToCatch(collidingObject);
            // Debug.Log(this+ " catching ball ");
            currentState = PlayerManager.catching;
        } else if(collidingObject.tag == "GameBall" && game.ball.GetComponent<Ball>().currentBallState == Ball.ballIsOut ) {
            // attemptToCatch(collidingObject);
            // Debug.Log(this+ " catching ball ");
            Debug.Log("trigger pick up");
            currentState = PlayerManager.grabbing;
        }else if(collidingObject.tag == "TryZoneA" || collidingObject.tag == "TryZoneB" && collidingObject.tag == tryZone.ToString() )
        {
            Debug.Log(this+" got to the tryzone!");
            if(isBallHolder )
            {
                currentState = PlayerManager.scoring;
            }
        }else
        {
            collidingObject = null;
        }
        
    }
     
    
}