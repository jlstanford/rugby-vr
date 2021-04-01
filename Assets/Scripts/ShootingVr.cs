using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingVr : GrabbableObjectVR
{
    public GameObject pelletPreFab;
    public float shootingForce;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnInteractionStarted() 
    {
        Instantiate(pelletPreFab,spawnPoint.position,spawnPoint.rotation);
    }
}
