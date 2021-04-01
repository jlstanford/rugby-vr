using System;
using System.Collections;
using UnityEngine;

public class AIHooker :  AIPlayer
{
 
    private float throwVal = 1.5f;
    private float moveVal = 15;
    void Start() 
    {
        throwForce = throwVal;
        moveSpeed = moveVal;
    } 

    void update()
    {
       
    }


}
