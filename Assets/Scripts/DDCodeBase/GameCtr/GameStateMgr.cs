using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMgr : Singleton<GameStateMgr>
{
    public static GameState CurGameState { get; private set; } = GameState.Standby;
    public static GameState LastGameState { get; private set; } = GameState.Standby;

    public enum GameState
    {
        Standby = 0,
        Loading,
        Gaming
    }

    public static void ChangeGameState(GameState gameState)
    {
        LastGameState = CurGameState;
        CurGameState = gameState;
    }
} 

