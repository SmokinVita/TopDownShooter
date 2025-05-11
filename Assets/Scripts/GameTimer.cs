using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timer;
    

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.StartTimer() == true)
        {
            Timer();
        }
    }

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
