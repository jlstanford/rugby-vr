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
    updatePossession(team);
    
    // playerState = PlayerState.STAGGERED;

}

void Update() 
{
    foreach(VRGrab handCmp in actionsCmps)
    {
        if(handCmp.heldObject != null) this.heldObject = handCmp.heldObject;
    }
    Debug.Log(this.game.getBall().GetComponent<Ball>());
    // if( //get child VRGrab scripts and see if HeldObject != null
    //     //this.game.getBall().GetComponent<Ball>().isBeingHeld
    //     )
    // {
    Debug.Log("GameBall is being Held!");
    if(this.heldObject != null && this.heldObject.tag == "GameBall")
    {
        this.isBallHolder = true;
        this.playerTeam.ballHolder = this;
    } else 
    {
        Debug.Log(this);
        Debug.Log(this.heldObject);
        }
    // }
  

}

public void setGame(Game game)
{
    this.game = game;
}

public void setTeam(TeamScript team)
{
    this.playerTeam = team;
}

public virtual void updatePossession(TeamScript team)
{
    
    // //set playerState

    if(team.teamState == TeamScript.TeamState.OFF){
        if(ball.GetComponent<Ball>().isBeingHeld == false)
        {
         playerState = PlayerState.STAGGERED;
        }else
        {
            playerState = PlayerState.BALL_CARRYING;
        }
        
    } else { playerState = PlayerState.FLAT; }
}

public virtual void scoreTry()
{
    //  playerTeam.updateScore( playerTeam.getScore() + 5 );
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
