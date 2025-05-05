using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    GameManager _gameManager;
    [SerializeField] private GameObject _pauseMenu;

    
    [SerializeField] private TMP_Text _attackSpdLvl;
    [SerializeField] private TMP_Text _speedLvl;
    [SerializeField] private TMP_Text _firewallLvl;
    [SerializeField] private TMP_Text _orbsLvl;
    [SerializeField] private TMP_Text _bulletStrUpgrade;

    private void Start()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _gameManager.PauseGame(true);
            _pauseMenu.SetActive(true);
        }
    }

    public void ResumeGamePlay()
    {
        _gameManager.PauseGame(false);
        _pauseMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        _gameManager.ReturnToMainMenu();
        _gameManager.PauseGame(false);
    }

    public void QuitGame()
    {
        _gameManager.QuitGame();
    }

    public void UpdateUpgradeLvl(string upgrade, int upgradelvl)
    {
        switch (upgrade)
        {
            case "AttackSpeed":
                _attackSpdLvl.SetText(upgradelvl.ToString());
                break;
            case "Speed":
                _speedLvl.SetText(upgradelvl.ToString());
                break;
            case "Firewall":
                _firewallLvl.SetText(upgradelvl.ToString());
                break;
            case "Orbs":
                _orbsLvl.SetText(upgradelvl.ToString());
                break;
            case "BulletStr":
                _bulletStrUpgrade.SetText(upgradelvl.ToString());
                break;

        }
    }
  
}
