using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
        Debug.Log("You scored!!!");
    // playerState = PlayerState.LINED_UP;
    // playerTeam.updatePossessionState();
    // isOffending = true;
        player.drop(player.game.ball.GetComponent<Ball>());
        player.playerManager.updatePossession(player.playerTeam);
        player.game.updateScoreFor(player.playerTeam, 5 );
    // game.reset();
        // player.touchDown();
        player.game.reset();
        return player.playerManager.linedUp;
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
