using System;
// using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWinger :  AIPlayer
{
 
    private float throwVal = 3f;
    private float moveVal = 30;
    void Start() 
    {
        throwForce = throwVal;
        moveSpeed = moveVal;
    } 

    void update()
    {
       
    }


}
