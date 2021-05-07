using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickingOff : GameState
{
    public GameState DoState(Game game)
    {
        return game.kickingoff;   
    }
}