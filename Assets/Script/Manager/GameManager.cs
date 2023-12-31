using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu = 0, Gameplay = 1, Finish = 2 };
public class GameManager : Singleton<GameManager>
{
    private GameState state;
    public Camera camera;

    private void Awake()
    {
        //ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return state == gameState;
    }


}
