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
    //set isbBallHolder
    // isBallHolder = false;
    
    // //set playerState
    if(team.teamState == TeamScript.TeamState.OFF){
    //     if(isBallHolder == false)
    //     {
         playerState = PlayerState.STAGGERED;
    //     }else
    //     {
    //         playerState = PlayerState.BALL_CARRYING;
    //     }
        
    } else { playerState = PlayerState.FLAT; }
    
    // playerState = PlayerState.STAGGERED;

}

public void setGame(Game game)
{
    this.game = game;
}

public void setTeam(TeamScript team)
{
    this.playerTeam = team;
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
