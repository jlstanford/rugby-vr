

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamScript : MonoBehaviour
{

    public int score;
    public Player[] players;
    public Player ballHolder;
    public Game game;
    public GameObject tryZone;
    // public TeamScript team;
    public TeamState teamState;
    [System.Serializable]
    public enum TeamState
    {
        DEF,
        OFF,
        SCRUMMING,
        LINED_UP,
        BALLS_OUT
    }

    // Start is called before the first frame update
    void Start()
    { 
       

    }

    public void init(Game game) 
    {
        Debug.Log("ts"+this);
        var thisTeam = this.ToString();
        setGame(game);
        setPossession(game);
        this.score = 0;
        if(teamState == TeamState.OFF)
        {
            tryZone = game.tryZoneA;
        } else { tryZone = game.tryZoneB; }
        foreach(Player player in players)
        {
            Debug.Log("TS"+player);
            player.init(game,this);
            // player.setTeam(this);
            // player.setGame(game);
            Debug.Log(player);
        }
    }

    public void setPossession(Game game)
    {
        if(game.offensiveTeam == this)
        {
            teamState = TeamScript.TeamState.OFF;
        }else 
        {
            teamState = TeamScript.TeamState.DEF;
        }
    }

    public void setGame(Game game)
    {
        this.game = game;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public int getScore()
    {
        return game.getScoreFor(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePossessionState()
    {
    //     only ballHolders team will have ballHolder defined
        if(this.ballHolder != null)
        {
           game.offensiveTeam = this;
           teamState = TeamScript.TeamState.OFF;
        }else if(this.ballHolder == null && game.ballHolder != null){
           game.defensiveTeam = this;
           teamState = TeamScript.TeamState.DEF;
        }
    }

    public void updateScore(int newScore)
    {
        this.score = newScore;
        this.game.updateScoreFor(this);
    }
}
