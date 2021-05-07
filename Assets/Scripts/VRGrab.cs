using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : ActionsScript
{
    
    private bool gripHeld;
    private VRInput controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<VRInput>();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.gripValue > 0.5f && gripHeld == false)
        {
            gripHeld = true;
            if (collidingObject && collidingObject.GetComponent<Rigidbody>())
            {
                heldObject = collidingObject;

                Grab();
            }
        }
        if (controller.gripValue < 0.5f && gripHeld == true)
        {
            gripHeld = false;
            if (heldObject)
            {
                Release();
            }
        }
    }

    public void Grab()
    {
        if(heldObject.GetComponent<Ball>()){
            var ball = heldObject;
            Debug.Log("VR Grabbing Ball!");
            // heldObject.transform.SetParent(this.transform);
            // heldObject.GetComponent<Rigidbody>().isKinematic = true;
            // heldObject.GetComponent<Ball>().isBeingHeld = true;
            //  heldObject = ball;
            Debug.Log(this+"Caught the Ball!"+ball.GetComponent<Ball>());
            ball.transform.SetParent(this.transform);
            // ball.transform.position.y = 1.0f;
            ball.transform.position = new Vector3(ball.transform.position.x,1.0f,ball.transform.position.z);
            // heldObject.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Ball>().isBeingPassed = false;
            ball.GetComponent<Ball>().isBeingHeld = true;
            ball.GetComponent<Ball>().isOut = false;
            ball.GetComponent<Ball>().currentBallState = Ball.ballIsBeingHeld;
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            player.isBallHolder = true;
            // playerManager.updatePossession(playerTeam);
            player.playerManager.ballHolder = player;
            player.heldObject = ball;
            player.currentState = PlayerManager.ballCarrying;
            // heldObject.GetComponent<Ball>().currentBallState = Ball.ballIsBeingHeld;
        }
        //if heldObject is Player and Player.isBallHolder == true - Player.PlayerState = PlayerState.Down
        if(heldObject.GetComponent<AIPlayer>()){
            Debug.Log("VR Grabbing Player :"+heldObject.GetComponent<Player>());
            player.tackle( heldObject.GetComponent<Player>() );
        }
    }

    public void Release()
    {
        //throw
        if(heldObject.GetComponent<Ball>()){
            var ball = heldObject.GetComponent<Ball>();
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            Debug.Log("VR velocity"+controller.velocity);
            Debug.Log("VR Ball is being thrown at:"+controller.velocity*throwForce);
            //float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
            rb.velocity = controller.velocity * throwForce;
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Ball>().isBeingHeld = false;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            ball.isBeingPassed = true;
            ball.isBeingHeld = false;
            ball.isOut = false;
            ball.currentBallState = Ball.ballIsBeingPassed;

            
            player.isBallHolder = false;
            heldObject.transform.SetParent(null);
            player.heldObject = null;
            
        }
        
        heldObject = null;
    }
}
