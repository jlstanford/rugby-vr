

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
    // public TeamScript team;
    public TeamState teamState;
    [System.Serializable]
    public enum TeamState
    {
        DEF,
        OFF,
        SCRUMMING,
        LINED_UP
    }

    // Start is called before the first frame update
    void Start()
    { 
       

    }

    public void init(Game game) 
    {
        var thisTeam = this.ToString();
        setGame(game);
        setPossession(game);
        this.score = 0;
        foreach(Player player in players)
        {
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
        return this.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void updatePossesionState(Game game)
    // {
    //     Debug.Log(game.defensiveTeam.ToString());
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
    // }

    // public void updateScore(int newScore)
    // {
    //     this.score = newScore;

    // }

    // public int getScore()
    // {
    //     return game.getScore();

    // }
}
