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
        //StartCoroutine(DisplayOptions());
        DisplayOptions();

    }

    //Get scriptable object upgrade and add to one button. 
    //call UpgradeHandler to get upgrade to display.

    private void DisplayOptions()
    {

        /*for (int i = 0; i < 3; i++)
        {
            UpgradeScriptableObject selectedUpgrade = _upgradeHandler.PickRandomUpgrade();
            foreach (var upgrade in _buttons)
            {
                if (upgrade.name == selectedUpgrade.name)
                {
                    Debug.Log($"Got the same name of {upgrade.name} and {selectedUpgrade.name}");
                    selectedUpgrade = _upgradeHandler.PickRandomUpgrade();
                    Debug.Log($"Got the same name of {upgrade.name} and {selectedUpgrade.name}");
                }
            }

        }*/

        for (int i = 0; i < 3; i++)
        {
            _selectedUpgrade.Add(_upgradeHandler.PickRandomUpgrade());
        }

        foreach (var item in _selectedUpgrade)
        {
            if (item.name == item.name)
            {
                //_selectedUpgrade.Remove(item);
               //_selectedUpgrade.Add(_upgradeHandler.PickRandomUpgrade());
            }
        }

        UpgradeImageUpdate();
        GameManager.Instance.PauseGame(true);
    }

    private void UpgradeImageUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            {
                UpgradeScriptableObject selectedUpgrade = _upgradeHandler.PickRandomUpgrade();
                _buttons[i].name = selectedUpgrade.name;

                _buttons[i].onClick.AddListener(() => { _player.AddUpgrade(selectedUpgrade.upgradeName); });
                _buttons[i].transform.GetChild(1).GetComponentInChildren<Image>().sprite = selectedUpgrade.upgradeImage;
                _buttons[i].GetComponentInChildren<TMP_Text>().SetText(selectedUpgrade.upgradeName);

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
