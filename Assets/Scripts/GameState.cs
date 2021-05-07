using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameState 
{
    // Start is called before the first frame update
    GameState DoState(Game game);
    
    

}