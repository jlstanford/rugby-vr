using System.Collections;
using UnityEngine;

public class AIActions : ActionsScript
{
    // public AIPlayer player; 
    // public AIActions()
    // {
    //     // this.player=GetComponent<AIPlayer>();
    // }
    // public AIActions(Player aiPlayer)
    // {
    //     this.player=(AIPlayer)aiPlayer;
    // }
    
    // public void init(Player aiPlayer)
    // {
    //     this.player=(AIPlayer)aiPlayer;
    // }
    // public void performActionFor(PlayerState state)
    // {
    //     // player = GetComponent<AIPlayer>();
    //     Debug.Log(player+"state"+state);
    //     switch(state.ToString())
    //     { 
    //         case "BALL_CARRYING":
    //             startBallCarrying();
    //             break;
    //         case "DOWN":
    //             goDown();
    //             break;
    //         case "LINED_UP":
    //             getLinedUp();
    //             break;
    //         case "FOLLOWING":
    //             startFollowing();
    //             break;
    //         // case "CATCHING": 
    //         //     attemptToCatch();
    //         //     break;
    //         case "FLAT": 
    //             getFlat();
    //             break;
    //         case "STAGGERED":
    //             getStaggered();
    //             break;
    //         case "CHASING":
    //             startChasing();
    //             break;
    //         // case "PICKING_UP":
    //         //     pickUp();
    //         //     break;
    //         case "ENGAGING":
    //             doPassDecision();
    //             break;
    //     }
    // }

    // public void startBallCarrying()
    // {
    //     // player = GetComponent<AIPlayer>();
    //         // GetComponent<AIPlayer>().tryToScore();
    //         Debug.Log("running toward ball carrying player "+player+"'s tryzone: "+player.tryZone);
    //         runToward(player.tryZone);
            
    // }
    public void goDown()
    {
        
    }
    // public void getLinedUp()
    // {
    //     // player = GetComponent<AIPlayer>();
    //     Debug.Log("Lined up player:"+player);
    //     player.lineUp();   
    // }
    // public void startFollowing()
    // {
    //     // player = GetComponent<AIPlayer>();
    //     Player ballHolder = player.playerManager.ballHolder;
    //     getStaggered();
        
    // }
    // public void startChasing()
    // {
    //     // player = GetComponent<AIPlayer>();
    //     // Debug.Log(player+"running toward player "+player.playerManager.ballHolder);
    //     runToward(player.game.ball);//player.playerManager.ballHolder.GetComponentInChildren<GameObject>
    // }
    public void getFlat()
    {
        
    }
    // public void getStaggered()
    // {
    //     // player = GetComponent<AIPlayer>();
    //     Player ballHolder = player.playerManager.ballHolder;
    //     if(player.isLeftOf(ballHolder))
    //     {
    //         Debug.Log(player+" is staggering right");
    //         player.transform.position  = new Vector3(ballHolder.transform.position.x - 15,0,ballHolder.transform.position.z - 15);
    //         // player.transform.position.z  = ;
    //     }else if(player.isRightOf(ballHolder))
    //     {
    //         Debug.Log(player+" is staggering left");
    //         player.transform.position  = new Vector3(ballHolder.transform.position.x + 15,0, ballHolder.transform.position.z - 15);
            
    //     }
    // }
    
    //TODO: create listeners for kickoff, reset, etc.,
    // public void attemptToCatch(GameObject ball)
    // {
    //     Debug.Log(this+" is Attempting to catch");
    //     // player = GetComponent<AIPlayer>();
    //     // GetComponent<AIPlayer>().catch_( ball);
    //     var rnd = new System.Random();
    //     // var randNum = rnd.Next();
    //     // if (randNum%2 == 0 ) {
    //         GetComponent<AIPlayer>().catch_( ball);
    //     // } else {
    //     // }
    // }
    // public void pickUp(GameObject ball)
    // {
    //     Debug.Log("Attempting to pick up");
    //     player = GetComponent<AIPlayer>();
    //     GetComponent<AIPlayer>().pickUp( ball);
    // }
    // public void doPassDecision()
    // {
    //     // player = GetComponent<AIPlayer>();
    //     // GetComponent<AIPlayer>().passTo( GetComponent<AIPlayer>().passReciever );
    //     var rnd = new System.Random();
    //     var randNum = rnd.Next();
    //     if (randNum%2 == 0 ) {
    //         player.passTo( player.nearestTeammate );
    //         Debug.Log("Passing to "+player.nearestTeammate);
    //         player.ball.GetComponent<Ball>().isBeingPassed = true;
    //         player.ball.GetComponent<Ball>().isOut = false;
    //         player.ball.GetComponent<Ball>().isOut = false;
    //         // player.playerState = PlayerState.STAGGERED;
    //     } else {
    //         Debug.Log("No pass; get hit! "+player.nearestTeammate+"- balls out");
    //         player.ball.GetComponent<Ball>().isBeingPassed = false;
    //         player.ball.GetComponent<Ball>().isOut = true;
    //         player.ball.GetComponent<Ball>().isOut = true;
    //     }
    // }

    // public override void OnTriggerEnter(Collider other)
    // {
    //     player = GetComponent<AIPlayer>();
    //     Debug.Log("trigger override");
    //     collidingObject = other.gameObject;
        
    //     Debug.Log("player performing trigger action"+GetComponent<AIPlayer>());
    //     Debug.Log("collding object tag:"+collidingObject.tag);
    //     Debug.Log("ballHolding player:"+GetComponent<AIPlayer>().isBallHolder);
    //     if(collidingObject.tag == $"{player.playerTeam} Player" && player.isBallHolder == true ) {


    //         Debug.Log("in go down trigger");
    //         // collidingObject = null;
    //         // player.beTackled();
    //         // player.drop(GetComponentInChildren<Ball>());
    //         StartCoroutine("getUpTimer");
    //     }else if(collidingObject.GetComponent<AIPlayer>() == player.playerManager.ballHolder && player.playerManager.ballHolder != null ) { // collidingObject.TryGetComponent<AIPlayer>(out AIPlayer collidingPlayer) == true
            
    //         Debug.Log("tackling player - "+collidingObject.GetComponent<AIPlayer>());
    //         player.tackle(collidingObject.GetComponent<AIPlayer>());
    //     }else if(collidingObject.tag == "GameBall" && collidingObject.GetComponent<Ball>().isBeingPassed && collidingObject.GetComponent<Ball>().isOut == false) {
    //         attemptToCatch(collidingObject);
    //     } else if(collidingObject.tag == "TryZoneA" || collidingObject.tag == "TryZoneB" )
    //     {
    //         Debug.Log(GetComponent<AIPlayer>()+" got to the tryzone!");
    //         if(player.isBallHolder )
    //         {
    //             GetComponent<AIPlayer>().touchDown();
    //         }
    //         //game.updateScores();
    //     }
        
    // }

    // public void OnCollisionEnter(Collision other) {
    //     if(other.GameObject.tag == "TryZone" )
    //     {
    //         Debug.Log("You got to the tryzone!");
    //         if(GetComponent<Player>().isBallHolder )
    //         {
    //             GetComponent<Player>().touchDown();
    //         }
    //         //game.updateScores();
    //     }
    // }

    // private IEnumerator getUpTimer()
    // {
    //     Debug.Log("waiting for 5 seconds");
    //     yield return new WaitForSeconds(5);
    // }

    // public void runToward(GameObject obj)
    // {
    //     player = GetComponent<AIPlayer>();
    //     Vector3 objPositionOnGround = new Vector3(obj.transform.position.x,0,obj.transform.position.z);
    //     player.transform.position = Vector3.MoveTowards(player.transform.position, objPositionOnGround ,player.moveSpeed * Time.deltaTime);
    //     Debug.Log("running toward tryzone");
    // }
}