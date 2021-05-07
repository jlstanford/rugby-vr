using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        Debug.Log("You scored!!!");
        // player.drop(player.game.ball.GetComponent<Ball>());
        player.playerManager.updatePossession(player.playerTeam);
        player.game.updateScoreFor(player.playerTeam, 5 );
        player.game.resetGame();
        return PlayerManager.linedUp;
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
