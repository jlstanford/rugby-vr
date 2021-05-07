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
    public List<Player> enemies = new List<Player>();
    public List<Player> teammates = new List<Player>();
    public bool isBallHolder = false;
    public Vector3 position;
    public Game.Side side;
    public Vector3 homePosition = new Vector3(50.0f,0.0f,50.0f);
    public GameObject heldObject = null;
    public bool isOffending = false;
    public PlayerManager playerManager = null;
    public float distanceFromNearestEnemy = 0.0f;
    public Player nearestEnemy = null;
    public Player nearestTeammate = null;
    public GameObject collidingObject = null;
    public Player actionTarget = null;
    public string previousStateName = "";
    public AIPlayerState currentState = PlayerManager.ready;
    public string currentStateName = "";
    public float chaseDistance = 50;

    



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

    
    this.transform.position = position;
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
    this.transform.position = new Vector3(homePosition.x,homePosition.y,homePosition.z);
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
    var dropPosition = this.transform.position;
    playerManager.ballHolder = null;
    game.ballHolder = null;
    isBallHolder = false;
    heldObject = null;

    ball.beDroppedBy(this);
    
}

public virtual void beTackled()
{
    Debug.Log(GetComponent<Player>()+" being tackled");
    playerState = PlayerState.DOWN;
    Debug.Log("Player is Down");
}

public void tryToScore()
{
    if(TryGetComponent<AIPlayer>(out AIPlayer aiPlayer) == true)
    {
        GetComponent<AIPlayer>().runToward(tryZone);
    }
}


    public virtual Player getNearestEnemy()
    {
        return playerManager.getNearestEnemyFor(this);
    }

    public virtual Player getNearestTeammate()
    {
        return playerManager.getNearestTeammateFor(this);

    }

    

    public virtual void passTo(Player targetPlayer)
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
        ball.GetComponent<Ball>().currentBallState = Ball.ballIsBeingHeld;
        ball.GetComponent<Rigidbody>().useGravity = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        isBallHolder = true;
        // playerManager.updatePossession(playerTeam);
        playerManager.ballHolder = this;
        heldObject = ball;
    }
    public virtual void pickUp(GameObject ball)
    {
        heldObject = ball;
        Debug.Log("Picked up the Ball!");
        isBallHolder = true;
        heldObject = ball;
        playerManager.ballHolder = this;
        playerManager.updatePossession(playerTeam);
        ball.GetComponent<Ball>().bePickedUpBy(this);
    }
    public virtual void lineUp()
    {
        Debug.Log(this+"calling goToStartPosition");
        goToStartPosition();
    }

    public virtual void getStaggered()
    {

    }
    // public override void tackle(Player targetPlayer)
    // {
    //     // targetPlayer.playerState = PlayerState.DOWN;
    //     // targetPlayer.drop(ball.GetComponent<Ball>());
    //     base.tackle(targetPlayer);
    // }


}
