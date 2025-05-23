using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPManagement : MonoBehaviour
{
    [SerializeField] private int _currentLvl = 1;
    [SerializeField] private float _currentXP = 0;
    [SerializeField] private float _expNeededToLvl = 10;

    private void Awake()
    {
        UIManager.Instance.ExpBar(_currentXP, _expNeededToLvl);
        UIManager.Instance.UpdateMaxExp(_expNeededToLvl);
    }

    public void AddExp(int xpAmount)
    {
        _currentXP += xpAmount;
        if (_currentXP >= _expNeededToLvl)
        {
            _currentXP -= _expNeededToLvl;
            _currentLvl++;
            _expNeededToLvl *= 2;
            UIManager.Instance.UpdateMaxExp(_expNeededToLvl);
            UIManager.Instance.OpenUpgradeMenu();
        }

        UIManager.Instance.ExpBar(_currentXP, _expNeededToLvl);
        // _maxEXP * (_currentxp/100);
    }
}
