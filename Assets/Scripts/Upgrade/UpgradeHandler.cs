using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    //Need to keep hold of all the upgrades
    [SerializeField] private List<UpgradeScriptableObject> _upgradeScriptableObjects = new List<UpgradeScriptableObject>();

    //need to pick 3 and display them

    public UpgradeScriptableObject PickRandomUpgrade()
    {
        int random = Random.Range(0, _upgradeScriptableObjects.Count);
        //Debug.Log($"We Got {_upgradeScriptableObjects[random].name}");
        return _upgradeScriptableObjects[random];
    }
}
