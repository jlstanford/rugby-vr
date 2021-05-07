using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPlayer : Player
{
    // public Player nearestEnemy;
    public float throwForce; 
    public float moveSpeed; 
    public Player passReciever;
    public float distanceFromBall;
    public float followDistance = 15;
    
    // public AIActions actionScript = new AIActions();
    // public AIPlayerState currentState;
    // public string currentStateName;
    // public float distanceFromNearestEnemy;
    // public Player nearestEnemy;
    // public Player nearestTeammate;
    // public Vector3  homePos;
    // public Team teamEnum;

    void Start()
    {
        // actionScript = new AIActions(this);
        currentState = PlayerManager.linedUp; 
       
    }

    // private void OnEnable()
    // {
    //     // this.side = playerManager.sides[playerTeam];
    //     // this.tryZone = playerManager.tryZoneAreas[side];
    // }
    void Update(){
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
    public bool isLeftOf(Player otherPlayer)
    {
        bool isLeft = false;
        if (position.x - otherPlayer.transform.position.x < 0)
        {
            isLeft = true;
        } 
        return isLeft;
    }
    public bool isRightOf(Player otherPlayer)
    {
        bool isRight = false;
        if (position.x - otherPlayer.transform.position.x > 0)
        {
            isRight = true;
        }
        return isRight;
    }

     public void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("trigger override");
        collidingObject = other.gameObject;
        
        Debug.Log("player performing trigger action"+GetComponent<AIPlayer>());
        Debug.Log("collding object tag:"+collidingObject.tag);
        Debug.Log("ballHolding player:"+GetComponent<AIPlayer>().isBallHolder);

        if(collidingObject.tag == $"{playerTeam} Player" && isBallHolder == true ) {


            Debug.Log("in go down trigger");
            currentState = PlayerManager.goingDown;
        }else if( collidingObject.GetComponent<AIPlayer>() == playerManager.ballHolder && playerManager.ballHolder != null ) { // collidingObject.TryGetComponent<AIPlayer>(out AIPlayer collidingPlayer) == true
            // collidingObject.GetComponent<AIPlayer>().playerTeam != this.playerTeam
            Debug.Log("tackling player - "+collidingObject.GetComponent<AIPlayer>());
            Debug.Log("tackling playerTeam - "+collidingObject.GetComponent<AIPlayer>().playerTeam);
            // tackle(collidingObject.GetComponent<AIPlayer>());
            base.actionTarget = collidingObject.GetComponent<AIPlayer>();
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
                // touchDown();
            }
            //game.updateScores();
        }else
        {
            collidingObject = null;
        }
        
    }
    public override void getStaggered()
    {
        // player = GetComponent<AIPlayer>();
        Player ballHolder = playerManager.ballHolder;
        if(this.isLeftOf(nearestTeammate)) //nearestTeammate
        {
            Debug.Log(this+" is staggering right");
            this.transform.position  = new Vector3(nearestTeammate.transform.position.x - 15,0,nearestTeammate.transform.position.z - 15);
            // player.transform.position.z  = ;
        }else if(this.isRightOf(nearestTeammate))//nearestTeammate
        {
            Debug.Log(this+" is staggering left");
            this.transform.position  = new Vector3(nearestTeammate.transform.position.x + 15,0, nearestTeammate.transform.position.z - 15);
            
        }
    }
    
    public void runToward(GameObject obj)
    {
        // Vector3 objPositionOnGround = new Vector3(obj.transform.position.x,0,obj.transform.position.z);
        // transform.position = Vector3.MoveTowards(transform.position, obj.GetComponent<Transform>().position ,moveSpeed * Time.deltaTime);
        Vector3 maxDistanceDelta = Vector3.MoveTowards(transform.position, obj.GetComponent<Transform>().position ,moveSpeed * Time.deltaTime);
        transform.position = new Vector3(maxDistanceDelta.x,0,maxDistanceDelta.z);
        // transform.position.x = transform.position.x;
        // tranform.position.y = 0;
        // transform.position.z = transform.postion.z;
        Debug.Log(this+"at"+transform.position+" is running toward "+obj+" at "+obj.GetComponent<Transform>().position);
    }

}