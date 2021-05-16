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
    public static BallIsOut ballIsOut = new BallIsOut();
    public static BallIsBeingHeld ballIsBeingHeld = new BallIsBeingHeld();
    public static BallIsReady ballIsReady = new BallIsReady();
    public static BallIsBeingKickedOff ballIsBeingKickedOff = new BallIsBeingKickedOff();
    public static BallIsBeingPassed ballIsBeingPassed= new BallIsBeingPassed();
    public Game game;
    public Player passingPlayer;

    public string previousBallStateName;
    public BallState currentBallState;
    public string ballStateName;
    public Player kickOffTarget;
    
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
        currentBallState = ballIsReady;
    }

    // Update is called once per frame
    void Update()
    {
        position = GetComponent<Transform>().position;
         
        previousBallStateName = currentBallState.ToString();
        currentBallState = currentBallState.DoState(this);
        ballStateName = currentBallState.ToString();
    }

    public void reset()
    {
        this.GetComponent<Transform>().SetParent(null);
        this.transform.position = game.middle;
        
        currentBallState = ballIsReady;
        Debug.Log("Reset-Kicking off");
        // kickoffLauncher.FireCannonAtPoint();
        
    }
    
    public void kickoff()
    {
        // isOut = false;
        // isBeingHeld = false;
        // isBeingPassed = true;
        // Player targetPlayer = game.playerManager.playerRegistry[game.playerManager.offensiveTeam][1];
        var targetPlayer = kickOffTarget;
        // Debug.Log(targetPlayer);
        var velocity = BallisticVelocity(targetPlayer.gameObject.transform.position, 65);
        // velocity = velocity*2;
        Debug.Log("Kickoff state - kicking off at " + targetPlayer + "  "+targetPlayer.transform.position+ " velocity " + velocity);

        //make this a person that is throwing
        // this.transform.SetParent(null);
        this.GetComponent<Rigidbody>().transform.position = transform.position;
        this.GetComponent<Rigidbody>().velocity = velocity;
        this.GetComponent<CapsuleCollider>().isTrigger = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().useGravity = true;
        // this.transform.position = transform.position;
        // isOut = true;
        // isBeingHeld = false;
        // isBeingPassed = false;
        currentBallState = ballIsBeingKickedOff;
        // isReady = false;
    }
    private Vector3 BallisticVelocity(Vector3 destination, float angle)
    {
        Vector3 dir = destination - transform.position; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal distance
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
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

    //  use lerp to get ball from player hand to teammate rigidbody ?
    public void BePassedBy(Player player,Vector3 point)
    {
        var velocity = player.calculatePassVelocity(point, 45);
        Debug.Log("Passing at " + point + " velocity " + velocity);
        this.transform.position = player.transform.position;
        this.GetComponent<Rigidbody>().velocity = velocity*player.GetComponent<AIPlayer>().throwForce;
        
        currentBallState = ballIsBeingPassed;
    }

    // will lock ball position to player hand
    public void bePickedUpBy(Player player)
    {
        
        this.transform.SetParent(player.transform);
        this.transform.position = new Vector3(player.transform.position.x,1,player.transform.position.z);
        currentBallState = ballIsBeingHeld;
    }

    public void beDroppedBy(Player player)
    {
        var dropPosition = player.transform.position;
        this.transform.position= new Vector3(dropPosition.x+10,0.05f,dropPosition.z+10);// y has to be slightly above 0.0f so that it wont fall into the ground, z has 5 bounce offset so it falls away fom player -- TODO: should fall in direction player is running
        Debug.Log("Ball Dropped at "+this.transform.position+"!");
        currentBallState = ballIsOut;
    }
}
