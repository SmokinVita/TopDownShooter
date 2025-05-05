using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private GameManager _gameManager;

    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _expBarSlider;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private UpgradeHandler _upgradeHandler;
    //[SerializeField] private List<UpgradeScriptableObject> _selectedUpgrade = new List<UpgradeScriptableObject>();
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameoverMenu;
    [SerializeField] private TMP_Text _timerDisplay;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdateMaxHealth(float maxHealth)
    {
        _healthBarSlider.maxValue = maxHealth;
    }

    public void HealthBar(float health)
    {
        _healthBarSlider.value = health;
    }

    public void UpdateMaxExp(float maxExp)
    {
        _expBarSlider.maxValue = maxExp;
    }

    public void ExpBar(float exp)
    {
        _expBarSlider.value = exp;
    }

    public void OpenUpgradeMenu()
    {
        _upgradeMenu.SetActive(true);
        DisplayOptions();

    }

    public void GameOverMenu()
    {
        _gameoverMenu.SetActive(true);
        _gameManager.PauseGame(true);
    }

    public void RestartLevel()
    {
        _gameManager.PauseGame(false);
        _gameManager.ReloadScene();

    }

    private void DisplayOptions()
    {
        for (int i = 0; i < 3; i++)
        {
            //_selectedUpgrade.Add(_upgradeHandler.PickRandomUpgrade());

            UpgradeScriptableObject _selectedUpgrade = _upgradeHandler.PickRandomUpgrade();
            _buttons[i].name = _selectedUpgrade.upgradeName;
            _buttons[i].onClick.AddListener(() => { _player.AddUpgrade(_selectedUpgrade.name); });
            _buttons[i].transform.GetChild(1).GetComponentInChildren<Image>().sprite = _selectedUpgrade.upgradeImage;
            _buttons[i].GetComponentInChildren<TMP_Text>().SetText(_selectedUpgrade.upgradeName);
        }

       // UpgradeImageUpdate();
        _gameManager.PauseGame(true);

        _upgradeHandler.ClearObjects();
    }

   /* private void UpgradeImageUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            {
                
                _buttons[i].name = _selectedUpgrade[i].upgradeName;
                Debug.Log($"Button{i} got {_selectedUpgrade[i].upgradeName}");
                _buttons[i].onClick.AddListener(() => { _player.AddUpgrade(_selectedUpgrade[i].name); });
                _buttons[i].transform.GetChild(1).GetComponentInChildren<Image>().sprite = _selectedUpgrade[i].upgradeImage;
                _buttons[i].GetComponentInChildren<TMP_Text>().SetText(_selectedUpgrade[i].upgradeName);
                
            }
        }
    }*/

    public void RemoveListeners()
    {
        for (int i = 0; i < 3; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
        }
        _gameManager.PauseGame(false);
    }

    public void UpdateTimerDisplay(float timer)
    {
        float seconds = Mathf.FloorToInt(timer % 60);
        float minutes = Mathf.FloorToInt((timer / 60) % 60);
        _timerDisplay.SetText($"{minutes}:{seconds}");
    }
}
