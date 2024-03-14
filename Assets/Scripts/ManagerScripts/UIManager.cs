using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _expBarSlider;
    [SerializeField] private GameObject _upgradeMenu;

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
        GameManager.Instance.PauseGame(true);
    }
}
