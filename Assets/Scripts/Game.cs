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
    public static TeamScript offensiveTeam;
    public static TeamScript defensiveTeam;
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
    
        this.gameStyle = GameStyle.SEVENS;
        
        TeamScript[] teams = GetComponentsInChildren<TeamScript>();
        // Team[] teams1 = [offensiveTeam,defensiveTeam];
        coinFlip(teams);


        foreach(TeamScript ts in teams)
        {
            ts.init(this);
            // ts.setGame(this);
            // ts.setPossession(this);
            Debug.Log(ts);
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

    // public TeamScript.TeamState getPossessionFor(TeamScript team)
    // {
       
    //     TeamScript.TeamState possession = TeamScript.TeamState.LINED_UP;
    //     if(offensiveTeam == team)
    //     {
    //         possession = TeamScript.TeamState.OFF;
    //     }else 
    //     {
    //         possession = TeamScript.TeamState.DEF;
    //     }
    //     return possession;
    // }

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

    public void updateScores()
    {

    }

    

}
