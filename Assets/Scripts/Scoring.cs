using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        //touchDown Anim
        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Scoring"))
        {
            return PlayerManager.scoring;
        }else{
           Debug.Log("You scored!!!");
            player.isBallHolder = false;
            player.playerManager.ballHolder = null;
            
            // player.stopForAnim();
            // player.StartCoroutine(stopMovingTimer());
            // player.playerManager.updatePossession(player.playerTeam);  TODO: uncomment for next iteration; team with possession will be the team receiving the kickoff
            player.game.updateScoreFor(player.playerTeam, 5 );
            player.game.resetGame();
            return PlayerManager.linedUp; 
        }
        
    } 
    private IEnumerator stopMovingTimer()
    {
        Debug.Log("waiting for 7 seconds");
        yield return new WaitForSecondsRealtime(7);
    }
}
