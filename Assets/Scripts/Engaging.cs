using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engaging : AIPlayerState
{
    public AIPlayerState DoState(Player player)
    {
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if (randNum%2 == 0 ) {
           return PlayerManager.passing;
        } else {
            Debug.Log("No pass; get hit! "+player.nearestTeammate+"- balls out");
            // player.ball.GetComponent<Ball>().isBeingPassed = false;
            // player.ball.GetComponent<Ball>().isOut = true;
            // player.ball.GetComponent<Ball>().isOut = true;
            player.GetComponent<Player>().drop(player.ball.GetComponent<Ball>());
            return PlayerManager.down;
        }
        // return this;
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
