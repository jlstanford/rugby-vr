using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinedUp : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
          // player = GetComponent<AIPlayer>();
        Debug.Log("Lined up player:"+player);
        player.GetComponent<Player>().lineUp();   
        return PlayerManager.ready;
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
