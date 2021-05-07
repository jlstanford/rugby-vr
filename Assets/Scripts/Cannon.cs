using UnityEngine;

public class Cannon : MonoBehaviour
{
    // [SerializeField]
    // private Rigidbody cannonballInstance;
   
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    [Range(10f, 80f)]
    private float angle = 45f;


    public float passForce;
    public GameObject targetObject;
    public Vector3 destinationOffset;
    // public Ball ball;

    private void Start()
    {
     
        // InvokeRepeating("FireCannonAtPoint", 0.2f, 7.0f);
        
    }

    private void Update()
    {
        //
        // if( ball.GetComponent<Ball>().isBeingHeld == true ) //ball is not a child of the cannon
        // {
        //     //dontkeepspawningball
        //     CancelInvoke();
        // }
        // else if( ball.GetComponent<Ball>().isBeingHeld !=  true) 
        // {
        //     // repeatspawn
        //     if(IsInvoking("FireCannonAtPoint") == false)
        //     {
        //         InvokeRepeating("FireCannonAtPoint", 0.2f, 7.0f);
        //     }
            
        // }
            
    }

    public void FireCannonAtPoint()//Vector3 point
    {
        // targetObject.transform.position
        //GetComponent<Player>().nearestTeammate.transform.position
        var point = GetComponent<Player>().isOffending == true  ? GetComponent<Player>().getNearestTeammate().transform.position: GetComponent<Player>().getNearestEnemy().transform.position;
        var velocity = BallisticVelocity(point+destinationOffset, angle);
        velocity = velocity * passForce;
        Debug.Log("Cannon launching at " + point + " velocity " + velocity);

        //make this a person that is throwing
        ball.GetComponent<Rigidbody>().transform.position = transform.position;
        ball.GetComponent<Rigidbody>().velocity = velocity;
        ball.GetComponent<Ball>().isBeingPassed = true;
        ball.GetComponent<Ball>().isOut = false;
       
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
}
