using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catching : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
         Debug.Log(player+" is Attempting to catch");
        // player = GetComponent<AIPlayer>();
        // GetComponent<AIPlayer>().catch_( ball);
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if(player.TryGetComponent<AIPlayer>(out AIPlayer aiPlayer) )
        {
            if (randNum%2 == 0 ) {
            player.GetComponent<Player>().catch_(player.game.ball);
            player.playerManager.updatePossession(player.playerTeam);
            return PlayerManager.ballCarrying;
            } else {
                return PlayerManager.chasing;
            }
        }else if(player.TryGetComponent<VRPlayer>(out VRPlayer vrPlayer) )
        {
            if(player.heldObject != null && player.heldObject.TryGetComponent<Ball>(out Ball ball) == true) 
            {
                return PlayerManager.ballCarrying;
            } else 
            {
                return PlayerManager.chasing;
            }
        } else
        {
            return this;
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
