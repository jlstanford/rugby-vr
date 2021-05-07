using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BallState 
{
    // Start is called before the first frame update
    BallState DoState(Ball ball);
    
    

}
