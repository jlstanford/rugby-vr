using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectVR : MonoBehaviour
{
    public VRInput controller;
    public bool isBeingHeld;
    public Vector3 grabOffset;

    // Update is called once per frame
    public virtual void OnInteractionStarted()
    {
        
    }
    public virtual void OnInteractionUpdated()
    {
        
    }
    public virtual void OnInteractionEnded()
    {
        
    }
}
