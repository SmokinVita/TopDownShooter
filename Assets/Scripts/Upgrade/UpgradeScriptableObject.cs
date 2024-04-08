using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScritableObjects/UpgradeObject", order = 1)]
public class UpgradeScriptableObject : ScriptableObject
{
    public string upgradeName;
    public Sprite upgradeImage;

    public int attackSpeed;
    public int healthUpgrade;
    public int fireSpeed;
}
