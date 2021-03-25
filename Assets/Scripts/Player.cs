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
}

public virtual void findBallInGame(Game game)
{
    ball = game.getBall();
}

public void init()
{
    //player sets itself up
    // where its supposed to be, finds where the ball is, finds where the enemies are 
    // Debug.Log(playerTeam);
    // findBallInGame(playerTeam.game);
    playerTeam = GetComponentInParent<TeamScript>();
    actionsCmps = GetComponentsInChildren<VRGrab>();
    // foreach( TeamScript team in GetComponentsInParent<TeamScript>() )
    // {
    //     Debug.Log("Player "+team);
    // }

}

void update()
{
    // findBallInGame(playerTeam.game);

    // if(isBallHolder)
    // {
    //     playerTeam.teamState = TeamScript.TeamState.OFF;
    // } else { playerTeam.teamState = TeamScript.TeamState.OFF;}
    
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
