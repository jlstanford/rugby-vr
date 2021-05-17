using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passing : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        if(player.TryGetComponent(out AIPlayer aiplayer))
        {
            player.passTo( aiplayer.nearestTeammate );
            Debug.Log(player +"Passing to "+player.nearestTeammate);
        }
        // player.ball.GetComponent<Ball>().isBeingPassed = true;
        // player.ball.GetComponent<Ball>().isOut = false;
        // player.ball.GetComponent<Ball>().isBeingHeld = false;
        // player.playerState = PlayerState.STAGGERED;
        // player.nearestTeammate.currentState = PlayerManager.catching;
        return PlayerManager.staggered;
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
