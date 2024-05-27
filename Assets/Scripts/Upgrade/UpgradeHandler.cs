using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{

    //Need to keep hold of all the upgrades
    [SerializeField] private List<UpgradeScriptableObject> _upgradeScriptableObjects = new List<UpgradeScriptableObject>();
    [SerializeField] private List<UpgradeScriptableObject> _useableObjects = new List<UpgradeScriptableObject>();
    [SerializeField] private List<UpgradeScriptableObject> _selectedSO = new List<UpgradeScriptableObject>();
    private int _currentIndex = -1;

    //need to pick 3 and display them

    private void Awake()
    {
        ClearObjects();
    }

    public UpgradeScriptableObject PickRandomUpgrade()
    {
        //_selectedSO.Clear();
        int random = Random.Range(0, _useableObjects.Count);
        //Debug.Log($"We Got {_upgradeScriptableObjects[random].name}");
        //add Selected Objects to list if repeat remove one.
        _selectedSO.Add(_useableObjects[random]);
        _useableObjects.RemoveAt(random);
        /*foreach (var item in _selectedSO)
        {
            if (item == _upgradeScriptableObjects[random])
            {
                Debug.Log("Got same object");
                _selectedSO.Remove(item);
            }
        }*/
        _currentIndex++;
        return _selectedSO[_currentIndex];
    }

    public void ClearObjects()
    {
        _useableObjects.Clear();
        _selectedSO.Clear();

        for (int i = 0; i < _upgradeScriptableObjects.Count; i++)
        {
            _useableObjects.Add(_upgradeScriptableObjects[i]);
        }
    }
}
