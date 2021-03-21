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

    private int hondaScore;
    private int carnivalScore;
    public Team offensiveTeam;
    public Team defensiveTeam;
    public GameObject ball;
    private GameStyle gameStyle;

    public GameController controller;

    [System.Serializable]
    public enum GameStyle
    {
        SEVENS,
        FIFTEENS
    }

    void Start()
    {
        // this.offensiveTeam = Team.TEAM_HONDA;
        // this.defensiveTeam = Team.TEAM_CARNIVAL;
        this.gameStyle = GameStyle.SEVENS;
        coinFlip();
        TeamScript[] teamScripts = GetComponentsInChildren<TeamScript>();
        foreach(TeamScript teamScript in teamScripts)
        {
            if(teamScript.team.ToString()/*TeamScript*/ == offensiveTeam.ToString()+" (TeamScript)"/*Enum*/ )
            {
                teamScript.teamState = TeamScript.TeamState.OFF;
            // } else 
            // {
            //     this.teamState = TeamScript.TeamState.DEF;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getBall()
    { 
        return this.ball;
    }

    public void setOffense(Team team)
    {
        offensiveTeam = team;

    }

    public void setDefense(Team team)
    {
         defensiveTeam = team;
    }

    public void coinFlip()
    {
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if (randNum%2 == 0 ) {
            setOffense(Team.TeamCarnival);
            setDefense(Team.TeamHonda);
        } else {
            setOffense(Team.TeamHonda);
            setDefense(Team.TeamCarnival); 
        }
    }

    public void updateScores()
    {

    }

}
