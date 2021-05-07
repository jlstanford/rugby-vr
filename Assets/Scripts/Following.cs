using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following: AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // Player ballHolder = player.playerManager.ballHolder;

        player.GetComponent<Player>().getStaggered();
        return PlayerManager.following;
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
