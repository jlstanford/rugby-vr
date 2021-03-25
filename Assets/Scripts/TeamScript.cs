

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TeamScript : MonoBehaviour
{

    public int score;
    public Player[] players;
    public Game game;
    public TeamScript team;
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
        var thisTeam = this.ToString();
        this.game = GetComponentInParent<Game>();  
        this.team = this;
        Debug.Log( "Starting TeamScript " +thisTeam);
        this.score = 0;
        string offensiveTeamStr = Game.offensiveTeam.ToString(); //TODO: changes everytime for whatever reason, i'm annoyed
        //game knows team possesion statuses -- offensive team and defensive team fields
        Debug.Log("TeamScript's Game.offensiveTeam - "+offensiveTeamStr);
        Debug.Log($"TeamScript - {thisTeam.Contains(offensiveTeamStr)}");
        if(   thisTeam.Contains(offensiveTeamStr) )
        {
            Debug.Log("Setting TeamState as OFF for "+ thisTeam );
            this.teamState = TeamState.OFF;
        }else { 
            Debug.Log("Setting TeamState as DEF for "+ thisTeam );
            this.teamState = TeamState.DEF;
            }

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
