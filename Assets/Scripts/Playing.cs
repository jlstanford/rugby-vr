using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : GameState
{
    public GameState DoState(Game game)
    {   
        return game.playing;
    }
}