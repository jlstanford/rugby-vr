using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Player ballHolder = null;
    public Dictionary<Team,List<Player>> playerRegistry = new Dictionary<Team, List<Player>>();
    public List<Player> players = new List<Player>();
    public Game game = null;
    public Team offensiveTeam = Team.TEAM_CARNIVAL;
    public Team defensiveTeam = Team.TEAM_CARNIVAL;
    public Dictionary<Player,Vector3> playerPositions = new Dictionary<Player,Vector3>();
    public bool allPlayersAreRegistered = false;
    public Dictionary<Game.Side,GameObject> tryZoneAreas = new Dictionary<Game.Side,GameObject>();
    public Dictionary<Team,Game.Side> sides = new Dictionary<Team,Game.Side>();
    public static LinedUp linedUp = new LinedUp();
    public static BallCarrying ballCarrying = new BallCarrying();
    public static Catching catching = new Catching();
    public static Down down = new Down();
    public static Engaging engaging = new Engaging();
    public static Flat flat = new Flat();
    public static Following following = new Following();
    public static Passing passing = new Passing();
    public static Chasing chasing = new Chasing();
    public static Ready ready = new Ready();
    public static Tackling tackling = new Tackling();
    public static Scoring scoring = new Scoring();
    public static GoingDown goingDown = new GoingDown();
    public static Staggered staggered = new Staggered();
    public static Grabbing grabbing = new Grabbing();

    // public Dictionary<Player,Vector3> playerPositions;
   
    // public Dictionary<Player,Vector3> playerStaggeredPositions = new Dictionary<Player,Vector3>{
    //     {new AIWinger(),new Vector3()}
    // };
    // public Dictionary<Player,Vector3> playerFlatPositions = new Dictionary<Player,Vector3>();
    


    public void init(Game game)
    {
        this.game = game;
        playerRegistry = new Dictionary<Team, List<Player>>();
        
        // playerRegistry.AddTeamsFromGame(Team.TEAM_CARNIVAL,new List<Player>());
        // playerRegistry.Add(Team.TEAM_CARNIVAL,new List<Player>());
        // playerRegistry.Add(Team.TEAM_HONDA,new List<Player>());
        // players = getPlayers();
    }

    void Update()
    {
        // game.updatePlayerInfo(this);
        
    }

    public void update(Game game)
    {
        this.game=game;
        this.sides = game.sides;
        this.tryZoneAreas = game.tryZoneAreas;
        this.offensiveTeam = game.offensiveTeam;
        this.defensiveTeam = game.defensiveTeam;
        
        foreach (Player player in getPlayers())
        {
          player.update(this);
        }
        
    }

    public void updatePosition(Player player, Vector3 position)
    {
        playerPositions[player] = position;
    }
    public Player getPlayerAt(Vector3 position)
    {
        Player player = null;
        foreach(KeyValuePair<Player,Vector3> playerPositionEntry in playerPositions)
        {
            if(playerPositionEntry.Value == position)
            {
                player = playerPositionEntry.Key;
            }
        }
        return player;
    }
    public Vector3 getPlayerPosition(Player player)
    {
        return playerPositions[player];
    }
    public float getDistanceFromNearestEnemy(Player player)
    {
        float distanceFromNearestEnemy =  Vector3.Distance(player.transform.position, player.nearestEnemy.transform.position);
        // }
        Debug.Log(player+" distance from enemy"+player.nearestEnemy+" is "+distanceFromNearestEnemy);
        return distanceFromNearestEnemy;
        
    }

    public void updatePossession(Team offTeam)
    {   
        game.updatePossession(offTeam);
        offensiveTeam = game.offensiveTeam;
        defensiveTeam = game.defensiveTeam;
        //set player.isOffending for each player in offensiveTeam
        Debug.Log("possession update-offensive team on possession update: "+offensiveTeam);
        foreach(Player player in playerRegistry[game.offensiveTeam])
        {
            Debug.Log("possession update-Now offending player "+player+" for "+player.playerTeam);
            player.isOffending = true;
            if(player.isBallHolder == true)
            {
                // player.playerState = PlayerState.BALL_CARRYING;
                player.currentState = ballCarrying;
            }else if(player.isBallHolder != true)
            {
                // player.playerState = PlayerState.FOLLOWING;
                player.currentState = following;
            }
        }
        Debug.Log("possession update-defensive team on possession update: "+defensiveTeam);
        foreach(Player player in playerRegistry[game.defensiveTeam])
        {
            Debug.Log("possession update-Now defending player "+player+" for "+player.playerTeam);
            player.isOffending = false;
            // player.playerState = PlayerState.CHASING;
            player.currentState = chasing;
            
        }
    }
    public void setTryZone(GameObject tryZone, Team team)
    {
        //get players for team
        List<Player> players = playerRegistry[team]; 
        // for each player in team set try zone 
        foreach(Player player in players)
        {
            player.tryZone = tryZone;
        }
    }
    public void setPlayers(List<Player> players)
    {
        this.players = players;
    }

    public void setPlayerStates(List<Player> players,AIPlayerState playerState)
    {
        foreach(Player player in players)
        {
            // player.playerState = playerState;
            player.currentState = playerState;
            player.currentStateName = playerState.ToString();
            Debug.Log("set "+player+" state "+player.currentStateName);
        }
    }

    public void addTeamToRegistry(Team team, List<Player> players)
    {
        playerRegistry.Add(team,players);
        foreach(Player player in players)
        {
            player.transform.position = new Vector3(player.transform.position.x,0,player.transform.position.z);
            player.init(game,this,team);
            playerPositions[player] = player.transform.position;
            // Debug.Log(player);
        }
    }

    public void register(Player player,Team team) 
        {
            playerRegistry[team].Append(player);
        }

    public void resetPlayers()
    {
        foreach(Team team in playerRegistry.Keys)
        {
            playerRegistry[team].ForEach( player => {player.currentState = linedUp;} );
        }
    }

    public List<Player> getPlayers()
    {
        List<Player> playerList = new List<Player>();
        foreach(Team team in Enum.GetValues(typeof(Team)) )
        {
            // Debug.Log("team in get Players is "+team);
            playerList.AddRange(playerRegistry[team]);
        }
        return playerList;
    }
    
public Player getNearestEnemyFor(Player player)
    {
        //refactor to call playerManager.getNearestEnemy();
        var enemyDistances = getEnemyDistancesFor(player); 
        // Debug.Log("Enemy Distances: "+enemyDistances);
        // Player nearestEnemy;
        // var nearestEnemy = enemyDistances.minBy(pair => pair.value).key;
        var nearestEnemy = enemyDistances.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
        return nearestEnemy;
    }

public Dictionary<Player,float> getEnemyDistancesFor(Player player)
{
    //refactor to call playerManager.getEnemyDistances();
    // Debug.Log("getting enemy distances for "+player);
    Dictionary<Player,float>enemyDistances = new Dictionary<Player,float>();
    foreach(Player enemy in getEnemiesFor(player)) { 
        var enemyDistance = Vector3.Distance(enemy.position,player.position);
        // Debug.Log("enemy: "+enemy+" Distance: "+enemyDistance);
        enemyDistances.Add(enemy,enemyDistance);           
    }
    return enemyDistances;
}

public List<Player> getEnemiesFor(Player player)
{
    //refactor to call playerManager.getEnemies();
    // List<Player> enemies = new List<Player>();
    player.enemies = player.playerTeam == Team.TEAM_CARNIVAL ? playerRegistry[Team.TEAM_HONDA] : playerRegistry[Team.TEAM_CARNIVAL];
    // var enemies  ;
    // getEnemiesFor(this);
    Debug.Log("Enemies: "+player.enemies.ToString());
    return player.enemies;
}

public Player getNearestTeammateFor(Player player)
    {
        //refactor to call playerManager.getNearestEnemy();
        var teammateDistances = getTeammateDistancesFor(player); 
        // Debug.Log("Teammate Distances: "+teammateDistances);
        var nearestTeammate = teammateDistances.Aggregate((lowest, next) => lowest.Value < next.Value&& lowest.Key != player? lowest : next).Key; 
        return nearestTeammate;
    }

public Dictionary<Player,float> getTeammateDistancesFor(Player player)
{
    //refactor to call playerManager.getTeammateDistances();
    Dictionary<Player,float>teammateDistances = new Dictionary<Player,float>();
    foreach(Player teammate in getTeammatesFor(player)) { 
        if(teammate != player)
        {
        var teammateDistance = Vector3.Distance(teammate.position,player.position);
        teammateDistances.Add(teammate,teammateDistance);           
        }
    }
    return teammateDistances;
}
public List<Player> getTeammatesFor(Player player)
{
    //refactor to call playerManager.Teammates();
    // teammates = this.playerTeam.players;
    List<Player> teammates;
    teammates = player.playerTeam != Team.TEAM_CARNIVAL ? playerRegistry[Team.TEAM_HONDA] : playerRegistry[Team.TEAM_CARNIVAL];
    return teammates;
}


// public void resetPosition()
// {
//     if(playerTeam.tryZone.tag == "TryZoneA"){
//         this.transform.position = new Vector3(this.transform.position.x,0,565f);
//     }else if(playerTeam.tryZone.tag == "TryZoneB"){
//         this.transform.position = new Vector3(this.transform.position.x,0,495f);
//     }
// }



public virtual void updatePlayerState(Player player)//listen for possession update
{
    
    if(game.offensiveTeam == player.playerTeam){ 
        if(ballHolder != player)
        {
            // player.playerState = PlayerState.STAGGERED;
            player.currentState = staggered;
        }else
        {
            // player.playerState = PlayerState.BALL_CARRYING;
            player.currentState = ballCarrying;
        }
        
    } else if(player.playerTeam == game.defensiveTeam)
    { 
        // player.playerState = PlayerState.FLAT; 
        player.currentState = flat;
    }
    //player should be chasing object(player or ball)if ball is nearby(held or on ground)
    if(ballHolder != null){
        if(Vector3.Distance(ballHolder.position,player.position) < player.GetComponent<Player>().chaseDistance && player.isBallHolder == false) //&& this.game.ballHolder.playerTeam != team
        {
            // player.playerState = PlayerState.CHASING;
            player.currentState = chasing;
        }
    }
    // Debug.Log("distance from ball to player: "+Vector3.Distance(game.ball.GetComponent<Ball>().position,position) );
    
    // if(ballHolder == null && Vector3.Distance(game.ball.GetComponent<Ball>().position,player.position) < player.GetComponent<AIPlayer>().chaseDistance && game.ball.GetComponent<Ball>().isOut  && game.ball.GetComponent<Ball>().isBeingPassed == false){ 
    //     game.ball.GetComponent<Ball>().isOut = true;
    // } 
}


}