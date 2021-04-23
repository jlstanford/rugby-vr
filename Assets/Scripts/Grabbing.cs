using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing: AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
       //pick up colliding object 
       player.GetComponent<AIPlayer>().pickUp(player.game.ball);
       //set colliding object parent to player
       return player.playerManager.ballCarrying;
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
