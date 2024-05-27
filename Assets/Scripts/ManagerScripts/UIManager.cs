using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _expBarSlider;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private UpgradeHandler _upgradeHandler;
    [SerializeField] private List<UpgradeScriptableObject> _selectedUpgrade = new List<UpgradeScriptableObject>();
    [SerializeField] private Player _player;

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

    private void DisplayOptions()
    {
        for (int i = 0; i < 3; i++)
        {
            _selectedUpgrade.Add(_upgradeHandler.PickRandomUpgrade());     
        }

        UpgradeImageUpdate();
        GameManager.Instance.PauseGame(true);

        _upgradeHandler.ClearObjects();
    }

    private void UpgradeImageUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            {
                _buttons[i].name = _selectedUpgrade[i].name;

                _buttons[i].onClick.AddListener(() => { _player.AddUpgrade(_selectedUpgrade[i].upgradeName); });
                _buttons[i].transform.GetChild(1).GetComponentInChildren<Image>().sprite = _selectedUpgrade[i].upgradeImage;
                _buttons[i].GetComponentInChildren<TMP_Text>().SetText(_selectedUpgrade[i].upgradeName);
            }
        }
    }

    public void RemoveListeners()
    {
        for (int i = 0; i < 3; i++)
        {
            _buttons[i].onClick.RemoveAllListeners();
        }
    }
}
