using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

    //Type of Range enemies
    //Single shooter, multi bullet attack, spiral attack.
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private GameObject _firingPOS;
    [SerializeField] private float _attackCoolDown = 3f;
    private bool _canFire = true;

    private void Update()
    {
        if(GameManager.Instance.IsDead())
            return;

        if (Vector3.Distance(transform.position, _target.transform.position) < _distance && _canFire == true)
            Attack();
    }

    private void Attack()
    {
        Instantiate(_ammoPrefab, _firingPOS.transform.position, _firingPOS.transform.rotation);
        _canFire = false;
        StartCoroutine(FireCooldownRoutine());
    }

    IEnumerator FireCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCoolDown);
        _canFire = true;
    }
}
