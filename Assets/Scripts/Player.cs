using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    
    public PlayerState playerState;
    public Team playerTeam;
    public Game game;
    public GameObject tryZone;
    public GameObject ball;
    public MovementScript simpleCharacterController;
    public ActionsScript[] actionsCmps;
    public int playerNumber;
    public List<Player> enemies;
    public List<Player> teammates;
    public bool isBallHolder;
    public Vector3 position;
    public Game.Side side;
    public Vector3 homePosition;
    public GameObject heldObject;
    public bool isOffending;
    public PlayerManager playerManager;
    public float distanceFromNearestEnemy;
    public Player nearestEnemy;
    public Player nearestTeammate;
    public GameObject collidingObject ;
    public AIPlayer actionTarget = null;
    public string previousStateName;
    public AIPlayerState currentState;
    public string currentStateName;
    



    // Start is called before the first frame update
public virtual GameObject findBallInGame(Game game)
{
    // Debug.Log(game);
    return game.getBall();;
}

public void init(Game game, PlayerManager playerManager,Team team)
{
    this.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
    this.playerManager = playerManager;
    this.game = game;
    this.playerTeam = team;
    collidingObject = null;
    // setTeam(team);
    Debug.Log("Player:"+this+" Team: "+team);

    //set player positions
    // resetPosition();
    playerManager.register(this,playerTeam);
    // this.tag = "Player";
    this.tag = $"{playerTeam} Player";
    teammates = playerManager.getTeammatesFor(this);
    // TODO: refactor so players have teamName in their tags. Will help with game.getEnemiesFor()
    ball = game.ball;
    // chaseDistance = 50;
    // enemies = getEnemies();
    // playerState = PlayerState.STAGGERED;

}

void Update() 
{
    // Debug.Log("actionComponents "+actionsCmps);
    // foreach()

    
    this.transform.position = GetComponent<Transform>().position;
    playerManager.updatePosition(this,position);

   
}

public void update(PlayerManager playerManager)
{
    Debug.Log("Before update"+game.sides[playerTeam]+":"+playerTeam);
    this.playerManager = playerManager;
    this.game = playerManager.game;
    Debug.Log("After update"+game.sides[playerTeam]+":"+playerTeam);
    
    this.side = playerManager.sides[playerTeam];
    this.tryZone = playerManager.tryZoneAreas[side];
}

public void goToStartPosition()
{
    Debug.Log(this+"Going home:"+homePosition+" from "+transform.position);
    transform.position = homePosition;
}

public void setTeam(Team team)
{
    this.playerTeam = team;
}

// public void touchDown()
// {
//     //stop running
//     //score
//     Debug.Log("You scored!!!");
//     // playerState = PlayerState.LINED_UP;
//     // playerTeam.updatePossessionState();
//     // isOffending = true;
//     playerManager.updatePossession(playerTeam);
//     // drop(ball.GetComponent<Ball>());
//     game.updateScoreFor(playerTeam, 5 );
//     game.reset();
//     // game.resetGame();
    
// }


public virtual void tackle(Player playerBeingTackled){
    Debug.Log("tackling:"+playerBeingTackled+" do they have the ball? "+ playerBeingTackled.isBallHolder);
    if(playerBeingTackled.isBallHolder == true){
        var prevPlayerState = playerBeingTackled.playerState;
        playerBeingTackled.playerState = PlayerState.DOWN;
        playerBeingTackled.drop(ball.GetComponent<Ball>());
        playerBeingTackled.playerState = prevPlayerState;
    } else if (playerBeingTackled.isBallHolder == false)
    {
        Debug.Log("Tackle Miss");
        
    }
}


public void drop(Ball ball){
    ball.transform.SetParent(null); //game.transform
    playerManager.ballHolder = null;
    ball.transform.position= new Vector3(position.x,0,position.z+20);// 20 for the bounce offset so it falls away fom player
    Debug.Log("Ball Dropped!");
    ball.isOut = true;
    game.ballHolder = null;
    isBallHolder = false;
    heldObject = null;
    ball.isBeingHeld = false;
    
}

public void beTackled()
{
    Debug.Log(GetComponent<Player>()+" being tackled");
    playerState = PlayerState.DOWN;
    Debug.Log("Player is Down");
}

public void tryToScore()
{
    // if(GetComponent<AIActions>() != null)
    // {
    //     GetComponent<AIActions>().runToward(tryZone);
    // }
    GetComponent<AIPlayer>().runToward(tryZone);
}


}
