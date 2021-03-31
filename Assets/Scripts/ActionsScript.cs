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
    void start()
    {

    }

    void update()
    {
        
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        collidingObject = other.gameObject;
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == collidingObject)
        {
            collidingObject = null;
        }
    }
}