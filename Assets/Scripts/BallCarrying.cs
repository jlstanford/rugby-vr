using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCarrying : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // if(player.isBallHolder){
            // player.playerManager.updatePossession(player.playerTeam);
            Debug.Log("running toward ball carrying player "+player+"'s tryzone: "+player.tryZone);
            
            if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Down"))
            {
                return PlayerManager.down;
            } else if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Tackling"))
            {
                return PlayerManager.tackling;
            } else if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GoingDown"))
            {
                return PlayerManager.goingDown;
            } 
            else if(Vector3.Distance(player.transform.position, player.nearestEnemy.transform.position) < 2.0f  )//player.nearestEnemy.GetComponent<AIPlayer>().currentState.ToString() ==  "Tackling"
            {
                return PlayerManager.engaging;
            }else if(Vector3.Distance(player.transform.position, player.tryZone.transform.position) < 0.03f )//player.collidingObject.tag == "TryZoneA" || player.collidingObject.tag == "TryZoneB"
            {
                return PlayerManager.scoring;
            } 
            else if( player.GetComponent( typeof(Player) ) != null && player.heldObject != null && player.heldObject.tag == "GameBall" && !player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Down") )
            {
                if(player.TryGetComponent<AIPlayer>(out AIPlayer aiPlayer) == true)
                {
                   player.GetComponent<AIPlayer>().runToward(player.tryZone);  
                }
               
                //  
                return PlayerManager.ballCarrying;
            }
            else {
                return PlayerManager.chasing;
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
