using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP : MonoBehaviour
{

    [SerializeField] private int _expAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Give player exp! Make them strong!!!!!
            EXPManagement xp = other.GetComponent<EXPManagement>();
            if (xp != null)
            {
                xp.AddExp(_expAmount);
                Destroy(gameObject);
            }
        }
    }

}
