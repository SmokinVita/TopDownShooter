using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    private GameManager _gameManager;

    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private TMP_Text _healthPercentage;
    [SerializeField] private Slider _expBarSlider;
    [SerializeField] private TMP_Text _expPercentage;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private UpgradeHandler _upgradeHandler;
    //[SerializeField] private List<UpgradeScriptableObject> _selectedUpgrade = new List<UpgradeScriptableObject>();
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameoverMenu;
    [SerializeField] private TMP_Text _timerDisplay;

    [SerializeField] private Image _hurtOverlay;
    [SerializeField] private float _hurtDurationTimer = .5f;
    [SerializeField] private float _hurtDuration;
    [SerializeField] private float _hurtFadeTime = 2f;

    public bool _isGameRdy = false;
    [SerializeField] private CanvasGroup _fadePanel;
    [SerializeField] private float _fadeSpeed = 2f;
    public float _currentFadeTime = 0.0f;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        StartGame();
    }

    private void Update()
    {
        if (_hurtOverlay.color.a > 0)
        {
            Debug.Log("Called HurtOverLay");
            _hurtDuration += Time.deltaTime;
            if (_hurtDuration > _hurtDurationTimer)
            {
                float tempAlpha = _hurtOverlay.color.a;
                tempAlpha -= Time.deltaTime * _hurtFadeTime;
                _hurtOverlay.color = new Color(_hurtOverlay.color.r, _hurtOverlay.color.g, _hurtOverlay.color.b, tempAlpha);
            }

        }
    }

    public void UpdateMaxHealth(float maxHealth)
    {
        _healthBarSlider.maxValue = maxHealth;
    }

    public void HealthBar(float health)
    {
        //_healthBarSlider.value = health;
        _healthPercentage.SetText(health.ToString());
        _hurtDurationTimer = 0;
        if (health <= 0)
            return;
        _hurtOverlay.color = new Color(_hurtOverlay.color.r, _hurtOverlay.color.g, _hurtOverlay.color.b, .31f);
    }

    public void UpdateMaxExp(float maxExp)
    {
        _expBarSlider.maxValue = maxExp;
    }

    public void ExpBar(float exp, float maxExpNeeded)
    {
        //_expBarSlider.value = exp;
        float percentage = exp / maxExpNeeded * 100;
        Debug.Log($"Percentage{percentage}, currentXP: {exp}, Needed to lvl up: {maxExpNeeded}");
        _expPercentage.SetText(percentage.ToString());
        Debug.Log(exp);
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

    public void StartGame()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (_fadePanel.alpha > 0)
        {
            _fadePanel.alpha = Mathf.SmoothStep(1, 0, _currentFadeTime * _fadeSpeed);
            _currentFadeTime += Time.deltaTime;
            yield return null;
        }
        _isGameRdy = true;
        yield return null;
    }

    public bool StartTimer()
    {
        return _isGameRdy;
    }
}
