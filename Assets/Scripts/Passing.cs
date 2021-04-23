using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passing : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        player.GetComponent<AIPlayer>().passTo( player.nearestTeammate );
        Debug.Log("Passing to "+player.nearestTeammate);
        player.ball.GetComponent<Ball>().isBeingPassed = true;
        player.ball.GetComponent<Ball>().isOut = false;
        player.ball.GetComponent<Ball>().isBeingHeld = false;
        // player.playerState = PlayerState.STAGGERED;
        player.nearestTeammate.currentState = player.playerManager.catching;
        return player.playerManager.staggered;
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
