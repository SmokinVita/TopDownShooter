using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScritableObjects/UpgradeObject", order = 1)]
public class UpgradeScriptableObject : ScriptableObject
{
    public string upgradeName;

    public int attackSpeed;
    public int healthUpgrade;
    public int fireSpeed;
}
