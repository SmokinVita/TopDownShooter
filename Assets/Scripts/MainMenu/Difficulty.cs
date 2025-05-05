using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    public void Easy()
    {
        _gameManager.GameDifficulty(GameManager.Difficulty.Easy);
    }

    public void Medium()
    {
        _gameManager.GameDifficulty(GameManager.Difficulty.Medium);
    }

    public void Hard()
    {
        _gameManager.GameDifficulty(GameManager.Difficulty.Hard);
    }
}
