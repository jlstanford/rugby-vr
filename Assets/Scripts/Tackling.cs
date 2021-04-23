using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
        // if(player.actionTarget.playerTeam == player.playerTeam)
        // {
        //     return player.playerManager.staggered;
        // }
        // else
        // {
            if(player.collidingObject.TryGetComponent<AIPlayer>(out AIPlayer targetPlayer) == true)
            {
                targetPlayer.currentState = player.playerManager.down;
            }
            player.collidingObject.GetComponent<AIPlayer>().drop(player.collidingObject.GetComponent<AIPlayer>().ball.GetComponent<Ball>());
            return player.playerManager.chasing;
        // }
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
