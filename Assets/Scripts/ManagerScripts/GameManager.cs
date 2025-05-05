using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{

    public enum Difficulty { Easy, Medium, Hard };

    //Bool to know if player is alive or not Plus method to update that infomation.
    private bool _isDead;
    private bool _gameIsPaused = false;
    public Difficulty _currentDifficulty;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PlayerHasDied(bool playState)
    {
        _isDead = playState;
        UIManager.Instance.GameOverMenu();
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused == true)
        {
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
        else if (isPaused == false)
        {
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
    }

    public bool IsGameActive()
    {
        return _gameIsPaused;
    }

    public void GameDifficulty(Difficulty difficulty)
    {
        _currentDifficulty = difficulty;
    }

    public Difficulty Setting()
    {
        return _currentDifficulty;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadScene()
    {
        _isDead = false;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void QuitGame()
    {
        if (Application.isEditor)
        {
            Debug.Log("In Editor, Trying to quit game...........");
        }
        else
        {
            Application.Quit();
        }
    }
}
