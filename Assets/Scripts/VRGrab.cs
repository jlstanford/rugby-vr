﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRGrab : ActionsScript
{
    // /// <summary>
    // /// What we're colliding with
    // /// </summary>
    // public GameObject collidingObject;
    // /// <summary>
    // /// What we're holding
    // /// </summary>
    // public GameObject heldObject;
    // public float throwForce;
    private bool gripHeld;
    private VRInput controller;
    // private void OnTriggerEnter(Collider other)
    // {
    //     collidingObject = other.gameObject;
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject == collidingObject)
    //     {
    //         collidingObject = null;
    //     }
    // }
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<VRInput>();
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
            Debug.Log("Grabbing Ball!");
            heldObject.transform.SetParent(this.transform);
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.GetComponent<Ball>().isBeingHeld = true;
        }
        //if heldObject is Player and Player.isBallHolder == true - Player.PlayerState = PlayerState.Down
        if(heldObject.GetComponent<Player>()){
            Debug.Log("Grabbing Player :"+heldObject.GetComponent<Player>());
            this.GetComponentInParent<Player>().tackle( heldObject.GetComponent<Player>() );
        }
    }

    public void Release()
    {
        //throw
        if(heldObject.GetComponent<Ball>()){
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            Debug.Log(controller.velocity);
            Debug.Log("Ball is being thrown at:"+controller.velocity*throwForce);
            //float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
            rb.velocity = controller.velocity * throwForce;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Ball>().isBeingHeld = false;
        }
        
        heldObject = null;
    }
}
