using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        Debug.Log(player+ " ON the ball:"+(Vector3.Distance(player.transform.position, player.game.ball.transform.position) < 0.1f));
        Debug.Log(player+ " On the ball - distance: "+Vector3.Distance(player.transform.position, player.game.ball.transform.position));
        Debug.Log(player+ " On the ball - playerPos: "+player.transform.position);
        Debug.Log(player+ " On the ball - ballPos: "+ player.game.ball.transform.position);
        player.GetComponent<AIPlayer>().runToward(player.game.ball);
        if( Vector3.Distance(player.transform.position, player.game.ball.transform.position) < 0.5f && player.game.ball.GetComponent<Ball>().isOut == true)
        {
            return player.playerManager.grabbing;
        }
        else if(Vector3.Distance(player.transform.position, player.game.ball.transform.position) < 2.0f && player.game.ball.GetComponent<Ball>().isBeingHeld == true)
        {
            return player.playerManager.tackling;
        }else
        {
            return player.playerManager.chasing;
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
