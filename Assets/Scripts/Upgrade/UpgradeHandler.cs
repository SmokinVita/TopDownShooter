using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    //Need to keep hold of all the upgrades
    [SerializeField] private List<UpgradeScriptableObject> _upgradeScriptableObjects = new List<UpgradeScriptableObject>();
    [SerializeField] private List<UpgradeScriptableObject> _selectedSO = new List<UpgradeScriptableObject>();

    //need to pick 3 and display them

    public UpgradeScriptableObject PickRandomUpgrade()
    {
        //_selectedSO.Clear();
        int random = Random.Range(0, _upgradeScriptableObjects.Count);
        //Debug.Log($"We Got {_upgradeScriptableObjects[random].name}");
        //add Selected Objects to list if repeat remove one.
        /*_selectedSO.Add(_upgradeScriptableObjects[random]);
        foreach (var item in _selectedSO)
        {
            if (item == _upgradeScriptableObjects[random])
            {
                Debug.Log("Got same object");
                _selectedSO.Remove(item);
            }
        }*/
        
        return _upgradeScriptableObjects[random];
    }
}
