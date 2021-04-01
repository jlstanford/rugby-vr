using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [System.Serializable]
    public enum Team
    {
        TeamHonda,
        TeamCarnival
    }

    // private int hondaScore;
    // private int carnivalScore;
    public int[] scores;
    public Player ballHolder;
    public TeamScript offensiveTeam;
    public TeamScript defensiveTeam;
    public GameObject ball;
    private GameStyle gameStyle;
    public GameObject tryZoneA;
    public GameObject tryZoneB;
    public TeamScript playerTeam;  
    public TeamScript enemyTeam;  
    // public TeamScript[] teams;

    // public GameController controller;

    [System.Serializable]
    public enum GameStyle
    {
        SEVENS,
        FIFTEENS
    }

    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x,0,this.transform.position.z);
        this.gameStyle = GameStyle.SEVENS;
        this.scores = new int[2];
        TeamScript[] teams = GetComponentsInChildren<TeamScript>();
        Ball ballScript = GetComponentInChildren<Ball>();
        ballScript.init(this);
        coinFlip(teams);
        foreach(TeamScript ts in teams)
        {
            ts.init(this);
            
            //if team array has the XRRig player
            if(Array.Exists(ts.players,element => element.ToString().Contains("XRRig")) == true)
            {
                this.playerTeam = ts;
            } else if(Array.Exists(ts.players,element => element.ToString().Contains("XRRig")) == false ){ this.enemyTeam = ts;}
            Debug.Log(ts);
            
        }
        Debug.Log(playerTeam);
        Debug.Log(enemyTeam);
        setScores(playerTeam,enemyTeam);
     
        
    }

    // Update is called once per frame
    void Update()
    {
         foreach(TeamScript ts in GetComponentsInChildren<TeamScript>())
         {
            if(ts.ballHolder != null) this.ballHolder = ts.ballHolder;
         }
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

    //playerTeam is always the first(zero) index
    public void setScores(TeamScript playerTeam, TeamScript opposingTeam)
    {
        int playerTeamScore = playerTeam.getScore();
        int opposingTeamScore = enemyTeam.getScore();
        this.scores[0] = playerTeamScore;
        this.scores[1] = opposingTeamScore;
    }

    public int[] getScores()
    {
        return scores;
    }

    public int getScoreFor(TeamScript team)
    {
        int score = 0;
        if(team.ToString() == playerTeam.ToString() ){
            score = scores[0];
        } else if(team.ToString() == enemyTeam.ToString()){
            score = scores[1];
        } 
        return score;
    }

    public Player[] getEnemiesFor(Player player)
    {
        var pTeam = player.playerTeam;
        if(pTeam == this.playerTeam)
        {
            return enemyTeam.players;
        } else { return playerTeam.players; }
    }

    public void setOffense(TeamScript team)
    {
        offensiveTeam = team;

    }

    public void setDefense(TeamScript team)
    {
        defensiveTeam = team;
    }

    public void coinFlip(TeamScript[] teams)
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

    public void updateScoreFor(TeamScript team)
    {
        if(team == playerTeam)
        {
            scores[0] = team.score;
        } else {scores[1] = team.score;}
        //scores[0] = team.score
        // setScores(team,enemyTeam);
    }

    

}
