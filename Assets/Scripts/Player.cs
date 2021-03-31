using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float throwForce;
    public float moveSpeed;
    public PlayerState playerState;
    public TeamScript playerTeam;
    public Game game;
    public GameObject ball;
    public MovementScript simpleCharacterController;
    public ActionsScript[] actionsCmps;
    public Player nearestEnemy;
    Player[] enemies;
    public bool isBallHolder;
    public Vector3 position;
    public float chaseDistance;
    public float distanceFromNearestEnemy;
    public GameObject heldObject;
    public float distanceFromBall;



    // Start is called before the first frame update
public virtual GameObject findBallInGame(Game game)
{
    // Debug.Log(game);
    return game.getBall();;
}

public void init(Game game, TeamScript team)
{
    setGame(game);
    setTeam(team);
    this.tag = "Player";
    // this.tag = $"{playerTeam}Player";
    // TODO: refactor so players have teamName in their tags. Will help with game.getEnemiesFor()
    ball = game.ball;
    chaseDistance = 30;
    // enemies = getEnemies();
    // playerState = PlayerState.STAGGERED;

}

void Update() 
{
    // Debug.Log("actionComponents "+actionsCmps);
    // foreach()
    position = GetComponent<Transform>().position;
    //test without headset
    // this.heldObject = this.game.getBall();
    distanceFromBall = Vector3.Distance(game.ball.GetComponent<Ball>().position,position);
    // Debug.Log("GameBall is "+ball);
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
    //AI Stuff
if(TryGetComponent<AIActions>(out AIActions aiActions))
{
    nearestEnemy = getNearestEnemy();
    distanceFromNearestEnemy =  Vector3.Distance(position, nearestEnemy.position);
    if(isBallHolder){ //TODO: change to a while
        Debug.Log("running ");
        tryToScore();
        if(distanceFromNearestEnemy < 20 )
        {
            Debug.Log("AI Pass");
            aiActions.doPassDecision();  
        }
    }else if(isBallHolder == false && playerState == PlayerState.CHASING )
    {
        aiActions.runToward(game.ball); //make this trygetcomponent for xrrig or (better) refactor so only ais do this
    }
}
   
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
        
    } else if(team.teamState == TeamScript.TeamState.DEF)
    { 
        playerState = PlayerState.FLAT; 
    }
    //player should be chasing object(player or ball)if ball is nearby(held or on ground)
    if(game.ballHolder != null){
        if(Vector3.Distance(game.ballHolder.position,position) < chaseDistance ) //&& this.game.ballHolder.playerTeam != team
        {
            playerState = PlayerState.CHASING;
        }
    }
    // Debug.Log("distance from ball to player: "+Vector3.Distance(game.ball.GetComponent<Ball>().position,position) );
    
    if(game.ballHolder == null && Vector3.Distance(game.ball.GetComponent<Ball>().position,position) < chaseDistance && ball.GetComponent<Ball>().isOnGround  && ball.GetComponent<Ball>().isBeingPassed == false){ 
        playerState = PlayerState.CHASING;
    } 
}

public void scoreTry()
{
     playerTeam.updateScore( playerTeam.getScore() + 5 );
}

public Player getNearestEnemy()
    {
        var enemyDistances = getEnemyDistances(); 
        Debug.Log("Enemy Distances: "+enemyDistances);
        // Player nearestEnemy;
        // var nearestEnemy = enemyDistances.minBy(pair => pair.value).key;
        var nearestEnemy = enemyDistances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        return nearestEnemy;
    }

public Dictionary<Player,float> getEnemyDistances()
{
    Dictionary<Player,float>enemyDistances = new Dictionary<Player,float>();
    foreach(Player enemy in getEnemies()) { 
        var enemyDistance = Vector3.Distance(enemy.position,this.position);
        enemyDistances.Add(enemy,enemyDistance);           
    }
    return enemyDistances;
}

public Player[] getEnemies()
{
    enemies = game.getEnemiesFor(this);
    Debug.Log("Enemies: "+enemies.ToString());
    return enemies;
}

public virtual void tackle(Player playerBeingTackled){
    if(playerBeingTackled.isBallHolder){
        playerBeingTackled.playerState = PlayerState.DOWN;
        playerBeingTackled.drop(ball.GetComponent<Ball>());
    }
}


public void drop(Ball ball){
    // isBallHolder = false;
    ball.isBeingHeld = false;
    ball.transform.position= new Vector3(GetComponent<Player>().position.x,0,GetComponent<Player>().position.z+10);// 10 for the bounce offset so it falls away fom player
    ball.transform.SetParent(GetComponent<AIPlayer>().game.transform); 
    // ball = null;
    // this.ball = null;
    isBallHolder = false;
    heldObject = null;
    //TODO: refactor to Game.ballsOut();
    // playerTeam.teamState = TeamScript.TeamState.BALLS_OUT;
}

public void beTackled()
{
    Debug.Log(GetComponent<Player>()+"being tackled");
    playerState = Player.PlayerState.DOWN;
    Debug.Log("Player is Down");
}

public void tryToScore()
{
    if(GetComponent<AIActions>() != null)
    {
        GetComponent<AIActions>().runToward(playerTeam.tryZone);
    }
}


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
