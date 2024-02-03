using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    //Type of Range enemies
    //Single shooter, multi bullet attack, spiral attack.
    [SerializeField] private GameObject _ammoPrefab;
    private bool _canFire = true;

    private void Update()
    {
        if(GameManager.Instance.isDead())
            return;

        if (Vector3.Distance(transform.position, _target.transform.position) < _distance && _canFire == true)
            Attack();
    }

    private void Attack()
    {
        Instantiate(_ammoPrefab, transform.position, Quaternion.identity);
        _canFire = false;
        StartCoroutine(FireCooldownRoutine());
    }

    IEnumerator FireCooldownRoutine()
    {
        yield return new WaitForSeconds(3);
        _canFire = true;
    }
}
