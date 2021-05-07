using System.Collections;
using UnityEngine;

public class ActionsScript : MonoBehaviour 
{
     /// <summary>
    /// What we're colliding with
    /// </summary>
    public GameObject collidingObject;
    /// <summary>
    /// What we're holding
    /// </summary>
    public GameObject heldObject;
    public float throwForce;
    public Player player;
    void start()
    {
        collidingObject = null;
        player = GetComponentInParent<Player>();
    }

    void update()
    {
        
    }
    // public virtual void OnTriggerEnter(Collider other)
    // {
    //     collidingObject = other.gameObject;
    //     Debug.Log(GetComponentInParent<Player>()+" colliding with "+collidingObject.tag);
    // }
    public virtual void OnTriggerEnter(Collider other) {
        collidingObject = other.gameObject;
        Debug.Log("player"+player+"collided!");
        Debug.Log(GetComponentInParent<Player>()+" colliding with "+collidingObject.tag);
        if(collidingObject.tag == "GameBall"  ) {
            
            Debug.Log("VR pick up");
            // player.currentState = PlayerManager.grabbing;
        }else if( collidingObject.tag == "TryZoneA" || collidingObject.tag == "TryZoneB" ) //&& collidingObject.tag == player.tryZone.ToString() 
        {
            Debug.Log(player+" got to the tryzone!");
            if(player.isBallHolder )
            {
                player.currentState = PlayerManager.scoring;
                // player.touchDown();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == collidingObject)
        {
            collidingObject = null;
        }
    }
}