using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingDown : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
        return player.playerManager.down;
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
