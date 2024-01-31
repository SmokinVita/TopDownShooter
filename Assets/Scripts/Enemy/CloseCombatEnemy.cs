using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombatEnemy : Enemy
{
    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < _distance)
            Attack();
    }

    private void Attack()
    {
        Debug.Log("Attacking!");
    }
}
