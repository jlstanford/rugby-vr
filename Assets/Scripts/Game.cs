using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    // [System.Serializable]
    // public enum Team
    // {
    //     TeamHonda,
    //     TeamCarnival
    // }

    // private int hondaScore;
    // private int carnivalScore;
    public int[] scores;
    public List<Player> players;
    public Game game;
    public PlayerManager playerManager;

    public Player ballHolder;
    public Team offensiveTeam;
    public Team defensiveTeam;
    public List<Player> teamHondaPlayers = new List<Player>();
    public List<Player> teamCarnivalPlayers = new List<Player>();
    public GameObject ball;
    private GameStyle gameStyle;
    public GameObject tryZoneA;
    public GameObject tryZoneB;
    public Vector3 middle = new Vector3(50,0,50);
    // public TeamScript playerTeam;  
    // public TeamScript enemyTeam;  
    public Team[] teams;

    // public GameController controller;

    [System.Serializable]
    public enum GameStyle
    {
        SEVENS,
        FIFTEENS
    }
    [System.Serializable]
    public enum GameState
    {
        PAUSED,
        PLAYING,
        STOPPED,
        KICKOFF
    }
     public GameState currentGameState = GameState.STOPPED;
    public KickingOff kickingoff;
    public Playing playing;
    public Stopped stopped;
    
    [System.Serializable]
    public enum Side
    {
        SOUTH,
        NORTH
    }
    public Dictionary<Team,Side> sides = new Dictionary<Team,Side>();
    public Dictionary<Side,GameObject> tryZoneAreas = new Dictionary<Side,GameObject>();
    public Ball ballManager;
    public Bounds gameBounds;

/*
*
* public void init(Game game) 
    {
        
        this.score = 0;
    }
*
*

*/


    void Start()
    {
        setupArea();
        this.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
        this.gameStyle = GameStyle.SEVENS;
        this.scores = new int[2];
        // sides = new Dictionary<Team,Side>();
        tryZoneAreas = new Dictionary<Side,GameObject>{
            {Side.NORTH,tryZoneA},
            {Side.SOUTH,tryZoneB}
        };
        // this.playerManager = new PlayerManager();
        this.ballManager = ball.GetComponent<Ball>();
        this.playerManager = GetComponent<PlayerManager>(); //initialize playerManager
        playerManager.init(this);
        playerManager.addTeamToRegistry(Team.TEAM_CARNIVAL,teamCarnivalPlayers);
        // Debug.Log("Player registry"+playerManager.playerRegistry);
        playerManager.addTeamToRegistry(Team.TEAM_HONDA,teamHondaPlayers);
        playerManager.setPlayers(players);
        //register and init players
        // foreach(Player player in players)
        // {
        //     // Debug.Log("TS"+player);
        //     //put players in position
        //     player.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
        //     player.init(this,playerManager);
        //     // player.setTeam(this);
        //     // player.setGame(game);
        //     Debug.Log(player);
        // }
        teams = (Team[])Team.GetValues(typeof(Team));
        
        //Ball MANager
        Ball ballScript = GetComponentInChildren<Ball>();
        ballScript.init(this);
        coinFlip(teams); 
        // playerManager.setPlayerStates(players,playerManager.linedUp);
        setScore(Team.TEAM_CARNIVAL,0);
        setScore(Team.TEAM_HONDA,0);
        currentGameState = GameState.KICKOFF;
        updateComponents();
     
        
    }

    // Update is called once per frame
    void Update()
    {
        //  foreach(TeamScript ts in GetComponentsInChildren<TeamScript>())
        //  {
        //     if(ts.ballHolder != null) this.ballHolder = ts.ballHolder;
        //  }
        if(currentGameState == Game.GameState.KICKOFF)
        {
            // line up players
            playerManager.setPlayerStates(players,PlayerManager.linedUp);
            // call ball.kickOff();
            ball.GetComponent<Ball>().kickoff();
            currentGameState = Game.GameState.PLAYING;
        } 
        else if(currentGameState == Game.GameState.PAUSED)
        {
            //  save state - > go to pause scene
            //
        }
        else if(currentGameState == Game.GameState.PLAYING)
        {
            // do game play phases
            //
            foreach(Player player in players)
            {
                if( gameBounds.Contains(player.transform.position) == false )
                {
                    Debug.Log(player+" is out of Bounds");
                    // player.transform.position= game.gameBounds.ClosestPoint(player.transform.position);
                    player.goToStartPosition();
                    // game.resetGame();
                
                }
            }
            if( gameBounds.Contains(ball.transform.position) == false )
            {
                Debug.Log(ball+" is out of Bounds");
                // player.transform.position= game.gameBounds.ClosestPoint(player.transform.position);
                ball.transform.position = middle;
                // game.resetGame();
            
            }
        }
        else if(currentGameState == Game.GameState.STOPPED)
        {
            // go to start scene
            //
        }
        updateComponents();
    }

    public void updatePlayerInfo(PlayerManager playerManager)
    {
        this.ballHolder = playerManager.ballHolder;
        // this.offensiveTeam = playerManager.offensiveTeam;
    
    }

    public void setupArea()
    {   
        var width = 200;
        var height = 100000;
        var depth = 200;
        
        // boundArea
        gameBounds = new Bounds(new Vector3(0,0,0),new Vector3(width,height,depth));
        // middle = gameBounds.center;
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(middle,new Vector3(width,height,depth));

        // GameObject areaPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        // areaPlane.transform.position = new Vector3(0,0,0);
        // areaPlane.transform.localScale = new Vector3(10,0,10);
        // for(var x =0; x< width ; x++)
        // {
        //     for (var z = 0; z < depth; z++) //z for 3d plane
        //     {
        //         GameObject areaCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //         areaCube.transform.position = new Vector3(x,0,z); 
        //     }
            
        // }
    }

    public GameObject getBall()
    { 
        return this.ball;
    }

    public Player getBallHolder()
    {
        return ballHolder;
    }

    public void setBallHolder(Player player)
    {
        ballHolder = player;
    }

    public void setScore(Team team, int score)
    {
        this.scores[(int)team] = score;
    }

    public int[] getScores()
    {
        return scores;
    }

    public int getScoreFor(Team team)
    {
        return scores[(int)team];
    }

    // public Player[] getEnemiesFor(Player player)
    // {
    //     var pTeam = player.playerTeam;
    //     if(pTeam == this.playerTeam)
    //     {
    //         return enemyTeam.players;
    //     } else { return playerTeam.players; }
    // }

    public void resetGame() {
        currentGameState = GameState.KICKOFF;
        playerManager.resetPlayers();
        Debug.Log("reset players");
        // ballManager.getBall().reset();
        ball.GetComponentInChildren<Ball>().reset();
        // playerManager.setPlayerStates(players,playerManager.linedUp);
        Debug.Log("reset ball");
        playerManager.update(this);
    }

    public void setOffense(Team team)
    {
        Debug.Log("setOffense:"+team);
        offensiveTeam = team;
       
        //set tryZoneA
        sides[team] = Side.NORTH;

        // updateComponents(); // playerManager.offensiveTeam=offensiveTeam;
        //playerManager.setSide(team);
        // playerManager.setTryZone(tryZoneA,team);

    }

    public void setDefense(Team team)
    {
        Debug.Log("setDefense:"+team);
        defensiveTeam = team;
        //set tryZoneB
        sides[team] = Side.SOUTH;
        
        // updateComponents();
        //playerManager.setSide(team);
        // playerManager.setTryZone(tryZoneB,team);
    }

    public void updateComponents() 
    {
        playerManager.update(this);
    }

    public void coinFlip(Team[] teams)
    {
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if (randNum%2 == 0 ) {
            setOffense(teams[0]);
            setDefense(teams[1]);
        } else {
            setOffense(teams[1]);
            setDefense(teams[0]); 
        }
    }

    public void updateScoreFor(Team team, int pointsScored)
    {
        if(team == Team.TEAM_CARNIVAL)
        {
            scores[(int)Team.TEAM_CARNIVAL] += pointsScored;
        } else {scores[(int)Team.TEAM_HONDA] += pointsScored;}
        //scores[0] = team.score
        // setScores(team,enemyTeam);
    }

    public void updatePossession(Team offTeam)
    {
        Debug.Log("in update possession");
        Debug.Log((int)offTeam);
        switch( (int)offTeam)
        {
            case 0:
            {
                offensiveTeam = offTeam;
                defensiveTeam = teams[1];
                break;
            }
            case 1:
            {
                offensiveTeam = offTeam;
                defensiveTeam = teams[0];
                break;
            }
            default:
            {
                break;
            }
        }
    }

    // public void reset()
    // {
    //     set playerStatus = PlayerStatus.LINED_UP;
    //     playerManager.setPlayerStates(players,playerManager.linedUp);
    //     playerManager.update(this);
    //     kickOff();
    // }
    // public void ballsOut()
    // {
    //     foreach(Player player in players)
    //     {
    //         player.playerState = PlayerState.BALLS_OUT;
    //         // Debug.Log(team.teamState);
    //     }
    //     // playerTeam.updatePossessionState(PlayerState.BALLS_OUT);
    //     // enemyTeam.updatePossessionState(PlayerState.BALLS_OUT);
    //     Debug.Log("BALLS OUT!");
    // }

    

}
