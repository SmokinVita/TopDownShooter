using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //Bool to know if player is alive or not Plus method to update that infomation.
    private bool _isDead;

    public void PlayerHasDied(bool playState)
    {
        _isDead = playState;
        Debug.Log("Player has Dead!");
    }
    public bool IsDead()
    {
        return _isDead;
    }
}
