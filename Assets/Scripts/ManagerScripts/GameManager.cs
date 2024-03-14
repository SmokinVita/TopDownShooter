using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //Bool to know if player is alive or not Plus method to update that infomation.
    private bool _isDead;
    private bool _gameIsPaused = false;

    public void PlayerHasDied(bool playState)
    {
        _isDead = playState;
        Debug.Log("Player has Dead!");
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
        }
        else if (isPaused == false)
        {
            Time.timeScale = 1f;
        }
    }
}
