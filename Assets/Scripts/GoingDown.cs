using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingDown : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        // player.goToStartPosition();
        // player.stopForAnim();
        // player.StartCoroutine(stopMovingTimer());
        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("GoingDown"))
        {
            return PlayerManager.goingDown;
        }else {
            player.drop(player.game.ball.GetComponent<Ball>());
            return PlayerManager.down;
        }  
        
    } 
    private IEnumerator stopMovingTimer()
    {
        Debug.Log("waiting for 7 seconds");
        yield return new WaitForSecondsRealtime(7);
    }
}
