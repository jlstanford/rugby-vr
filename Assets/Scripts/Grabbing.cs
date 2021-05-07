using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing: AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        Debug.Log("grabbing ball state");
       //pick up colliding object 
       if(player.collidingObject!=null && player.collidingObject.tag == "GameBall")
       {
            player.GetComponent<Player>().pickUp(player.collidingObject); 
            return PlayerManager.ballCarrying; 
       } else
       {
            return PlayerManager.chasing;
       }
       
       //set colliding object parent to player
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
