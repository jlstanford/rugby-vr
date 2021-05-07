using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        
        getUpTimer();
        if(player.game.ball.GetComponent<Ball>().isOut == true)
        {
            return PlayerManager.chasing;
        }
        else if(player.playerManager.offensiveTeam == player.playerTeam)
        {
            return PlayerManager.staggered;
        }else
        {
            return PlayerManager.flat;
        }

       
    }
    private IEnumerator getUpTimer()
    {
        Debug.Log("waiting for 5 seconds");
        yield return new WaitForSeconds(5);
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
