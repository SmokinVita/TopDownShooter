using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timer;

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    //Create Timer, after so many minutes spawn one boss, double that and spawn the other boss. 
    //Like every 5 minutes spawn boss, at 15minutes spawn 2 of the first one and so on...
    //Call spawnmanager to spawn it or spawnmanager calls this one????

    private void Timer()
    {
        _timer += Time.deltaTime;
        UIManager.Instance.UpdateTimerDisplay(_timer);
        if (_timer % 90 == 0)
        {
            Debug.Log("Spawn Boss!");
        }
    }
}
