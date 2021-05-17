using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [System.Serializable]
public enum Team
{
    TEAM_CARNIVAL,
    TEAM_HONDA
}

static class TeamMethods
{
    public static Team getOpposingTeam(this Team team) {
        switch(team) {
            case Team.TEAM_CARNIVAL: return Team.TEAM_HONDA;
            case Team.TEAM_HONDA: return Team.TEAM_CARNIVAL;
            default: return team;
            // throw new IllegalStateException("This should never happen: " + this + " has no opposite.");
        }
    }
  
}
 