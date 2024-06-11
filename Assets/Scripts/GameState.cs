using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum GameConditions
    {
        GAME_PAUSED,
        GAME_PLAY,
        GAME_WON,
        GAME_LOST
    }

    public static GameConditions GameCondition;
}
