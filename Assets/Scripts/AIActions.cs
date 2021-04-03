using System.Collections;
using UnityEngine;

public class AIActions : ActionsScript
{
    
    public void attemptToCatch(GameObject ball)
    {
        Debug.Log("Attempting to catch");
        // GetComponent<AIPlayer>().catch_( ball);
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if (randNum%2 == 0 ) {
            GetComponent<AIPlayer>().catch_( ball);
        } else {
            //no pass
            //be Tackled 
        }
    }

    public void doPassDecision()
    {
        // GetComponent<AIPlayer>().passTo( GetComponent<AIPlayer>().passReciever );
        var rnd = new System.Random();
        var randNum = rnd.Next();
        if (randNum%2 == 0 ) {
            GetComponent<AIPlayer>().passTo( GetComponent<AIPlayer>().passReciever );
        } else {
            //no pass
            //be Tackled 
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger override");
        collidingObject = other.gameObject;
        
        Debug.Log(GetComponent<Player>());
        Debug.Log(collidingObject.tag);
        Debug.Log(GetComponent<Player>().isBallHolder);
        if(collidingObject.tag == "Player" && GetComponent<Player>().isBallHolder == true ) {

            Debug.Log("in go down trigger");
            collidingObject = null;
            GetComponent<Player>().drop(GetComponentInChildren<Ball>());
            GetComponent<Player>().beTackled();
            StartCoroutine("getUpTimer");
        } else if(collidingObject.tag == "GameBall" && collidingObject.GetComponent<Ball>().isBeingPassed) {
            attemptToCatch(collidingObject);
        } else if(collidingObject.tag == "TryZoneA" || collidingObject.tag == "TryZoneB" )
        {
            Debug.Log("You got to the tryzone!");
            if(GetComponent<Player>().isBallHolder )
            {
                GetComponent<Player>().touchDown();
            }
            //game.updateScores();
        }
        
    }

    // public void OnCollisionEnter(Collision other) {
    //     if(other.GameObject.tag == "TryZone" )
    //     {
    //         Debug.Log("You got to the tryzone!");
    //         if(GetComponent<Player>().isBallHolder )
    //         {
    //             GetComponent<Player>().touchDown();
    //         }
    //         //game.updateScores();
    //     }
    // }

    private IEnumerator getUpTimer()
    {
        Debug.Log("waiting for 5 seconds");
        yield return new WaitForSeconds(5);
    }

    public void runToward(GameObject obj)
    {
        Vector3 objPositionOnGround = new Vector3(obj.transform.position.x,0,obj.transform.position.z);
        GetComponent<AIPlayer>().transform.position = Vector3.MoveTowards(GetComponent<AIPlayer>().position, objPositionOnGround ,GetComponent<AIPlayer>().moveSpeed * Time.deltaTime);
        Debug.Log("running toward tryzone");
    }
}