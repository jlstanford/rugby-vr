using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopped : GameState
{
    public GameState DoState(Game game)
    {
        return game.stopped;
    }
}