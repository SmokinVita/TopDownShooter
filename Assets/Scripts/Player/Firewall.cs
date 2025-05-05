using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Firewall : MonoBehaviour
{

    [SerializeField] private float _cooldownHit = 3f;
    private float _timer;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
    }

    private void OnTriggerEnterStay(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        Debug.Log($"{other.name} is in trigger");
        if (damageable != null)
        {
            if (Time.time > _timer)
            {
                _timer = Time.time + _cooldownHit;
                damageable.Damage(1);
                Debug.Log("Firewall HIT!");
            }
        }
    }
}
