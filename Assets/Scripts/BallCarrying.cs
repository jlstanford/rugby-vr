using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCarrying : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // if(player.isBallHolder){
            player.playerManager.updatePossession(player.playerTeam);
            Debug.Log("running toward ball carrying player "+player+"'s tryzone: "+player.tryZone);
            player.GetComponent<AIPlayer>().runToward(player.tryZone);
            if(Vector3.Distance(player.transform.position, player.nearestEnemy.transform.position) < 15  )//player.nearestEnemy.GetComponent<AIPlayer>().currentState.ToString() ==  "Tackling"
            {
                return player.playerManager.engaging;
            }else if(Vector3.Distance(player.transform.position, player.tryZone.transform.position) < 0.1f )//player.collidingObject.tag == "TryZoneA" || player.collidingObject.tag == "TryZoneB"
            {
                return player.playerManager.scoring;
            } else 
            {
                return player.playerManager.ballCarrying; 
            }
        // }
        // else {
        //     return player.playerManager.staggered;
        // }
    
    
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
