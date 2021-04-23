using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : GrabbableObjectVR
{
    public Transform location;
    public Vector3 position;
    public Vector3 startPosition;
    public bool isBeingPassed;
    public bool isOut;
    public Game game;
    public Player passingPlayer;
    
    public void init(Game game)
    {
        
        this.game = game;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = position = GetComponent<Transform>().position;
        isBeingPassed = false;
        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        position = GetComponent<Transform>().position;
        if(position.y <= 0 && isBeingHeld == false && GetComponentInParent(typeof(AIPlayer)) == null)
        {
            isOut =true;
        }
    }

    public void reset()
    {
        position = startPosition;
        GetComponent<Transform>().SetParent(passingPlayer.transform);
    }
    public override void OnInteractionStarted()
    {
        //TODO: use trigger interact to aim a chip kick
        /* 
         * chip kicks will automatically have a much sharper angle than regular passes
         */
        // if (!isBeingHeld) 
        // {
        //     doPickUp();
        // }
        // if(isBeingHeld)
        // {
        //     // 

        // }
    }

    // will use lerp to get ball from player hand to teammate rigidbody
    void doPass()
    {

    }

    // will lock ball position to player hand
    void doPickUp()
    {
       
    }
}
