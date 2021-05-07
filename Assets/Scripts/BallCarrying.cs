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
            if(player.TryGetComponent<AIPlayer>(out AIPlayer aiPlayer) == true)
            {
               player.GetComponent<AIPlayer>().runToward(player.tryZone); 
            }
            if(Vector3.Distance(player.transform.position, player.nearestEnemy.transform.position) < 15  )//player.nearestEnemy.GetComponent<AIPlayer>().currentState.ToString() ==  "Tackling"
            {
                return PlayerManager.engaging;
            }else if(Vector3.Distance(player.transform.position, player.tryZone.transform.position) < 0.1f )//player.collidingObject.tag == "TryZoneA" || player.collidingObject.tag == "TryZoneB"
            {
                return PlayerManager.scoring;
            } else 
            {
                return PlayerManager.ballCarrying; 
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
