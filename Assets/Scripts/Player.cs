using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public float throwForce;
    public float moveSpeed;
    public TeamScript playerTeam;
    public VRJoystickMovement simpleCharacterController;
    public VRGrab[] actionsCmps;

    public bool isBallHolder;

    //A player should always know where the ball is 
    public GameObject ball;

    // Start is called before the first frame update
void Start()
{
    playerTeam = GetComponentInParent<TeamScript>();

    // Debug.Log(game.defensiveTeam.ToString());
    //     Debug.Log(game.offensiveTeam.ToString());
    //     Debug.Log(this.ToString());
    //     Debug.Log(game.defensiveTeam.ToString().Equals(this.ToString()));
    //     Debug.Log(game.offensiveTeam.ToString().Equals(this.ToString()));
    //     if(game.defensiveTeam.ToString().Equals(this.team.ToString()) )
    //     {
    //         this.teamState = TeamScript.TeamState.DEF;
    //     } else if( game.offensiveTeam.ToString().Equals(this.team.ToString()))
    //     {
    //         this.teamState = TeamScript.TeamState.OFF;
    //     }
    // actions = GetComponentInChildren<VRGrab>();
    // simpleCharacterController = GetComponentInChildren<VRJoystickMovement>();
}

void findBallInGame(Game game)
{
    this.ball = game.getBall();
}

public void init()
{
    //player sets itself up
    // where its supposed to be, where the ball is, where the enemies are 
    findBallInGame(playerTeam.game);
}

void update()
{
    findBallInGame(playerTeam.game);
    // foreach(VRGrab actionsCmp in actionsCmps)
    // {
    //     if( actionsCmp.heldObject.tag == "GameBall" && actionsCmp.heldObject != null)
    //     {

    //     ball = actionsCmp.heldObject;
    //     if(ball.GetComponent<Ball>().isBeingHeld){
    //         // 
    //         Debug.Log("Player knows ball is being held");
            
    //     }
    //     ////// update team possession status in game 
    //     //// if team possession is Defense
    //     ////// set players in team that arent ball carrying to PlayerStatus.FLAT
    //     }
    // }
    if(isBallHolder)
    {
        playerTeam.teamState = TeamScript.TeamState.OFF;
    } else { playerTeam.teamState = TeamScript.TeamState.OFF;}
    
}

void scoreTry()
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
