using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float throwForce;
    public float moveSpeed;
    public PlayerState playerState;
    public TeamScript playerTeam;
    public Game game;
    public GameObject ball;
    public VRJoystickMovement simpleCharacterController;
    public VRGrab[] actionsCmps;
    public bool isBallHolder;
    public Vector3 position;
    public float chaseDistance;
    public GameObject heldObject;

    // Start is called before the first frame update
public virtual GameObject findBallInGame(Game game)
{
    // Debug.Log(game);
    return game.getBall();;
}

public void init(Game game, TeamScript team)
{
   
    actionsCmps = GetComponentsInChildren<VRGrab>();
    simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
    setGame(game);
    setTeam(team);
    ball = game.ball;
    chaseDistance = 5f;
    // playerState = PlayerState.STAGGERED;

}

void Update() 
{
    foreach(VRGrab handCmp in actionsCmps)
    {
        if(handCmp.heldObject != null) this.heldObject = handCmp.heldObject;
    }
    //test without headset
    // this.heldObject = this.game.getBall();
    
    Debug.Log(this.game.getBall().GetComponent<Ball>());
    Debug.Log("GameBall is being Held!");
    if(this.heldObject != null && this.heldObject.tag == "GameBall")
    {
        this.isBallHolder = true;
        this.playerTeam.ballHolder = this;
        updatePlayerState(this.playerTeam); 
    }
     else 
    {
        Debug.Log(this);
        Debug.Log(this.heldObject);
    }
    playerTeam.updatePossessionState();
    updatePlayerState(this.playerTeam);
    
}

public void setGame(Game game)
{
    this.game = game;
}

public void setTeam(TeamScript team)
{
    this.playerTeam = team;
}

public virtual void updatePlayerState(TeamScript team)
{
    if(team.teamState == TeamScript.TeamState.OFF){
        if(isBallHolder == false)
        {
            playerState = PlayerState.STAGGERED;
        }else
        {
            playerState = PlayerState.BALL_CARRYING;
        }
        
    } else { playerState = PlayerState.FLAT; }
    //player should be chasing object(player or ball)if ball is nearby(held or on ground)
    if(game.ballHolder != null){
        if(Vector3.Distance(game.ballHolder.position,position) < 5f && this.game.ballHolder.playerTeam != team)
        {
            playerState = PlayerState.CHASING;
        }
    }
    Debug.Log("distance from ball to player: "+Vector3.Distance(game.ball.GetComponent<Ball>().position,position) );
    
    if(game.ballHolder == null && Vector3.Distance(game.ball.GetComponent<Ball>().position,position) < chaseDistance){ 
        playerState = PlayerState.CHASING;

    } 
}

public virtual void scoreTry()
{
     playerTeam.updateScore( playerTeam.getScore() + 5 );
}
void pass(Ball ball){}

void grab(Ball ball){}

[System.Serializable]
public enum PlayerState
{
    BALL_CARRYING,
    DOWN,
    STAGGERED,
    FLAT,
    CHASING
}

}
