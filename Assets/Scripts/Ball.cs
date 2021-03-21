using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : GrabbableObjectVR
{
    public Transform location;
    public Vector3 position;

    public Game game;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteraction()
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
