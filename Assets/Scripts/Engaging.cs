using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engaging : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        var rnd = new System.Random();
        var randNum = rnd.Next();
        // if aiplayer do the calcs
        if(player.TryGetComponent(out AIPlayer aiplayer))
        {
            if (randNum%2 == 0 ) {
                return PlayerManager.passing;
            } else {
                Debug.Log("No pass; get hit! "+player.nearestTeammate+"- balls out");
                // player.ball.GetComponent<Ball>().isBeingPassed = false;
                // player.ball.GetComponent<Ball>().isOut = true;
                // player.ball.GetComponent<Ball>().isOut = true;
                player.GetComponent<Player>().drop(player.ball.GetComponent<Ball>());
                return PlayerManager.goingDown;
            }
        }
        else if(player.TryGetComponent(out VRPlayer vrplayer))
        {
            if( vrplayer.isBallHolder && vrplayer.collidingObject != null && vrplayer.collidingObject.TryGetComponent( out Player enemyPlayer ) == true )
            {
                return PlayerManager.goingDown;
            }
            else {
                return PlayerManager.ballCarrying;
            }
        } else
        {
            return this;
        }
       
        
    }
    private IEnumerator stopMovingTimer()
    {
        Debug.Log("waiting for 7 seconds");
        yield return new WaitForSecondsRealtime(7);
    }
}
