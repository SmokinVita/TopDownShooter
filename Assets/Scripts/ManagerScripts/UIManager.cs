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

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i);
            UpgradeScriptableObject selectedUpgrade = _upgradeHandler.PickRandomUpgrade();
            
            _buttons[i].onClick.AddListener(() => { _player.AddUpgrade(selectedUpgrade.upgradeName); });
            _buttons[i].transform.GetChild(1).GetComponentInChildren<Image>().sprite = selectedUpgrade.upgradeImage;
            _buttons[i].GetComponentInChildren<TMP_Text>().SetText(selectedUpgrade.upgradeName);

        }    
        GameManager.Instance.PauseGame(true);
    }
}
