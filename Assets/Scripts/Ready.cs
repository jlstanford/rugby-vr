using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
        if(player.playerManager.ballHolder != null && player.playerManager.ballHolder.playerTeam != player.playerTeam)
        {
            return PlayerManager.chasing;
        }
        else if(player.game.ball.GetComponent<Ball>().currentBallState == Ball.ballIsOut)
        {
            return PlayerManager.chasing;
        }
         else if(player.playerManager.ballHolder != null && player.playerManager.ballHolder.playerTeam == player.playerTeam)
        {
            return PlayerManager.following;
        }else {
            return PlayerManager.ready;
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
