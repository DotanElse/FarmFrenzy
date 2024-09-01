using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance {get; private set;}
    public event EventHandler<OnGameStateChangeArgs> OnGameStateChange;

    public class OnGameStateChangeArgs : EventArgs
    {
        public GameState state;
    }
    public enum GameState {
        WaitingForGameStart,
        Countdown,
        Playing,
        GameOver
    }
    GameState gameState;
    float gameTime = 0;
    int waitingForGameStartTimer = 1;
    int countdownTimer = 3;
    int gameTimer = 120;
    void Start()
    {
        gameState = GameState.WaitingForGameStart;
        OnGameStateChange?.Invoke(this, new OnGameStateChangeArgs{
        state = gameState
        });
    }
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateState();
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public bool isGamePlaying()
    {
        return gameState == GameState.Playing;
    }
    public int getCountdownSeconds()
    {   
        int timeLeft = (int)Mathf.Ceil(countdownTimer - gameTime);
        return (int)Mathf.Max(0f, timeLeft);
    }

    public float getGametimePassedNormalized()
    {
        if(gameState == GameState.Playing)
            return Mathf.Max(1f - (gameTime/gameTimer), 0);
        return 0;
    }

    private void UpdateState()
    {
        switch(gameState)
        {
            case GameState.WaitingForGameStart:
                if(gameTime > waitingForGameStartTimer)
                {
                    gameTime = 0;
                    gameState = GameState.Countdown;
                    OnGameStateChange?.Invoke(this, new OnGameStateChangeArgs{
                    state = gameState
                    });
                }
                break;
            case GameState.Countdown:
                if(gameTime > countdownTimer)
                {
                    gameTime = 0;
                    gameState = GameState.Playing;
                    OnGameStateChange?.Invoke(this, new OnGameStateChangeArgs{
                    state = gameState
                    });
                }
                break;
            case GameState.Playing:
                if(gameTime > gameTimer)
                {
                    gameTime = 0;
                    gameState = GameState.GameOver;
                    OnGameStateChange?.Invoke(this, new OnGameStateChangeArgs{
                    state = gameState
                    });
                }
                break;
        }
        Debug.Log(gameState);
    }
}
