using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIPlayerState 
{
    // Start is called before the first frame update
    AIPlayerState DoState(Player player);
    
    

}
