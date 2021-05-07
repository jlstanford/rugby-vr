using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        
            if(player.collidingObject == null)
            {
                //Do nothing special
            }
            else if(player.collidingObject.TryGetComponent<Player>(out Player targetPlayer) == true)
            {
                targetPlayer.currentState = PlayerManager.goingDown;
                
            }
            return PlayerManager.chasing;
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
