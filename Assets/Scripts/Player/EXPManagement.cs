using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPManagement : MonoBehaviour
{
    [SerializeField] private int _currentLvl = 1;
    [SerializeField] private int _currentXP = 0;
    [SerializeField] private int _expNeededToLvl = 10;

    public void AddExp(int xpAmount)
    {
        _currentXP += xpAmount;
        if(_currentXP >= _expNeededToLvl)
        {
            _currentXP -= _expNeededToLvl;
            _currentLvl++;
            _expNeededToLvl *= 2;
        }
    }
}
