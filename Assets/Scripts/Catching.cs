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
        if (randNum%2 == 0 ) {
            player.GetComponent<AIPlayer>().catch_(player.collidingObject);
            player.playerManager.updatePossession(player.playerTeam);
            return player.playerManager.ballCarrying;
        } else {
            return player.playerManager.chasing;
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
