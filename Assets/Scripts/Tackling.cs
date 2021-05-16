using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackling : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
            // player.stopForAnim();
            // player.StartCoroutine(stopMovingTimer());
            if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Tackling"))
            {
                return PlayerManager.tackling;
            }
            if(player.collidingObject == null)
            {
                // Do nothing special
                return PlayerManager.chasing;
            }
            else if(player.collidingObject.TryGetComponent<Player>(out Player targetPlayer) == true)
            {
                player.collidingObject.GetComponent<Player>().currentState = PlayerManager.engaging;
            // targetPlayer.currentState = PlayerManager.goingDown;
                return PlayerManager.chasing;
                
            }else
            {
               // Do nothing special
                return PlayerManager.chasing;
            }
            
    } 
    private IEnumerator stopMovingTimer()
    {
        Debug.Log("waiting for 7 seconds");
        yield return new WaitForSecondsRealtime(7);
    }
}
