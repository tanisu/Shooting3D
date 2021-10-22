using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject titleSet = default;
    [SerializeField] GameObject gameClearSet = default;
    [SerializeField] GameObject gameOverSet = default;
    public enum GameState
    {
        TITLE,
        GAMEMAIN,
        CLEAR,
        GAMEOVER
    }
    private GameState gameState = GameState.TITLE;
    delegate void gameProc();
    Dictionary<GameState, gameProc> gameProcList;

    void Start()
    {
        gameProcList = new Dictionary<GameState, gameProc>
        {
            {GameState.TITLE,Title },
            {GameState.GAMEMAIN,GameMain },
            {GameState.CLEAR,Clear },
            {GameState.GAMEOVER,GameOver }
        };
        gameState = GameState.TITLE;
    }

    // Update is called once per frame
    void Update()
    {
        gameProcList[gameState]();
    }

    private void Title()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gameState = GameState.GAMEMAIN;
            StageController.I.StageStart();
            titleSet.SetActive(false);
        }
    }
    private void GameMain()
    {
        if (!StageController.I.isPlaying)
        {
            if(StageController.I.playStopCode == StageController.PlayStopCodeDef.PlayerDead)
            {
                gameOverSet.SetActive(true);
                gameState = GameState.GAMEOVER;
            }
            else
            {
                gameClearSet.SetActive(true);
                gameState = GameState.CLEAR;
            }
            
        }
    }
    private void Clear()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gameState = GameState.TITLE;
            gameClearSet.SetActive(false);
            titleSet.SetActive(true);
            StageController.I.ResetStage();
        }
    }
    private void GameOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gameState = GameState.TITLE;
            gameOverSet.SetActive(false);
            titleSet.SetActive(true);
            StageController.I.ResetStage();
        }
    }
}
