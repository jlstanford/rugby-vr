using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staggered  : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        player.GetComponent<Player>().getStaggered();
        if(player.isBallHolder == false && player.playerManager.offensiveTeam == player.playerTeam)
        {
            return PlayerManager.following;
        }
        else if(player.isBallHolder == false && player.playerManager.offensiveTeam != player.playerTeam)
        {
            return PlayerManager.chasing;    
        }
        else if(player.isBallHolder == true && player.playerManager.offensiveTeam == player.playerTeam)
        {
            player.playerManager.updatePossession(player.playerTeam);
            return PlayerManager.ballCarrying;    
        }
        else
        {
            return PlayerManager.staggered;    
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
