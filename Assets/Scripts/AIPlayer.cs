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
    
    public float chaseDistance = 50;
    public AIActions actionScript = new AIActions();
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
        currentState = playerManager.linedUp; 
       
    }

    // private void OnEnable()
    // {
    //     // this.side = playerManager.sides[playerTeam];
    //     // this.tryZone = playerManager.tryZoneAreas[side];
    // }
    void Update(){
        // chaseDistance = 50;
        this.side = playerManager.sides[playerTeam];
        this.tryZone = playerManager.tryZoneAreas[side];
        // Debug.Log(this+":"+side+":"+tryZone);
        
        // Debug.Log(this+" performing "+playerState+" action");


        // nearestEnemy = playerManager.getNearestEnemyFor(this);
        nearestEnemy = getNearestEnemy();
        // nearestTeammate = playerManager.getNearestTeammateFor(this);
        nearestTeammate = getNearestTeammate();
        // Debug.Log("distance for player" +this+" is:"+playerManager.getDistanceFromNearestEnemy(this));
        distanceFromNearestEnemy = playerManager.getDistanceFromNearestEnemy(this); 
        
        // GetComponent<AIActions>().performActionFor(playerState);
        previousStateName = currentState.ToString();
        currentState = currentState.DoState(this);
        currentStateName = currentState.ToString();
        // Debug.Log(currentState.ToString());
        
        
    }

    public Player getNearestEnemy()
    {
        return playerManager.getNearestEnemyFor(this);
    }

    public Player getNearestTeammate()
    {
        return playerManager.getNearestTeammateFor(this);

    }

    

    public virtual void passTo(Player targetPlayer)
    {
        ball.transform.SetParent(null);
        var point = targetPlayer.transform.position;
        Debug.Log("Passing to " + targetPlayer );
        var velocity = calculatePassVelocity(point, 45);
        // Debug.Log("Passing at " + point + " velocity " + velocity);
        velocity = velocity * throwForce;
        Debug.Log("Passing at " + point + " velocity " + velocity);

        //make this a person that is throwing
        ball.GetComponent<Rigidbody>().transform.position = transform.position;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        ball.GetComponent<Ball>().isBeingPassed = true;
        ball.GetComponent<Ball>().isBeingHeld = false;
        ball.GetComponent<Ball>().isOut = false;
        // ball.transform.SetParent(null); 
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
        heldObject = ball;
        Debug.Log(this+"Caught the Ball!"+ball);
        ball.transform.SetParent(this.transform);
        // ball.transform.position.y = 1.0f;
        ball.transform.position = new Vector3(ball.transform.position.x,1.0f,ball.transform.position.z);
        // heldObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Ball>().isBeingPassed = false;
        ball.GetComponent<Ball>().isBeingHeld = true;
        ball.GetComponent<Ball>().isOut = false;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        isBallHolder = true;
        // playerManager.updatePossession(playerTeam);
        playerManager.ballHolder = this;
        heldObject = ball;
    }
    public virtual void pickUp(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().isKinematic = true;
        heldObject = ball;
        Debug.Log("Picked up the Ball!");
        ball.transform.SetParent(this.transform);
        // ball.GetComponent<Transform>().position = this.position;
        // game.ball.transform.SetParent(this.transform);
        ball.GetComponent<Transform>().position = new Vector3(this.position.x,1,this.position.z);
        // heldObject.GetComponent<Rigidbody>().isKinematic = true;
        ball.GetComponent<Ball>().isBeingPassed = false;
        ball.GetComponent<Ball>().isBeingHeld = true;
        ball.GetComponent<Ball>().isOut = false;
        isBallHolder = true;
        heldObject = ball;
        playerManager.ballHolder = this;
    }
    public void lineUp()
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
            // collidingObject = null;
            // player.beTackled();
            // player.drop(GetComponentInChildren<Ball>());
            // StartCoroutine("getUpTimer");
            currentState = playerManager.goingDown;
        }else if( collidingObject.GetComponent<AIPlayer>() == playerManager.ballHolder && playerManager.ballHolder != null ) { // collidingObject.TryGetComponent<AIPlayer>(out AIPlayer collidingPlayer) == true
            // collidingObject.GetComponent<AIPlayer>().playerTeam != this.playerTeam
            Debug.Log("tackling player - "+collidingObject.GetComponent<AIPlayer>());
            Debug.Log("tackling playerTeam - "+collidingObject.GetComponent<AIPlayer>().playerTeam);
            // tackle(collidingObject.GetComponent<AIPlayer>());
            base.actionTarget = collidingObject.GetComponent<AIPlayer>();
            base.collidingObject = collidingObject;
            currentState = playerManager.tackling;
        }else if(collidingObject.tag == "GameBall" && collidingObject.GetComponent<Ball>().isBeingPassed && collidingObject.GetComponent<Ball>().isOut == false) {
            // attemptToCatch(collidingObject);
            // Debug.Log(this+ " catching ball ");
            currentState = playerManager.catching;
        } else if(collidingObject.tag == "TryZoneA" || collidingObject.tag == "TryZoneB" )
        {
            Debug.Log(this+" got to the tryzone!");
            if(isBallHolder )
            {
                currentState = playerManager.scoring;
                // touchDown();
            }
            //game.updateScores();
        }else
        {
            collidingObject = null;
        }
        
    }
    public void getStaggered()
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
        Vector3 objPositionOnGround = new Vector3(obj.transform.position.x,0,obj.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, objPositionOnGround ,moveSpeed * Time.deltaTime);
        Debug.Log("running toward "+obj);
    }

}