using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private GameManager _gameManager;
    private int _dmgMultiply = 1;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (_gameManager == null)
            Debug.Log($"{name} can't find GameManger!");

        switch (_gameManager.Setting())
        {
            case GameManager.Difficulty.Easy:
                _dmgMultiply = 1;
                break;
            case GameManager.Difficulty.Medium:
                _dmgMultiply = 2;
                break;
            case GameManager.Difficulty.Hard:
                _dmgMultiply = 3;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(2 * _dmgMultiply);
    }
}
