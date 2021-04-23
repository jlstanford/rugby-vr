using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staggered  : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        player.GetComponent<AIPlayer>().getStaggered();
        if(player.isBallHolder == false && player.playerManager.offensiveTeam == player.playerTeam)
        {
            return player.playerManager.following;
        }
        else if(player.isBallHolder == false && player.playerManager.offensiveTeam != player.playerTeam)
        {
            return player.playerManager.chasing;    
        }
        else if(player.isBallHolder == true && player.playerManager.offensiveTeam == player.playerTeam)
        {
            player.playerManager.updatePossession(player.playerTeam);
            return player.playerManager.ballCarrying;    
        }
        else
        {
            return player.playerManager.staggered;    
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
