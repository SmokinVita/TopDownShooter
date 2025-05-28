using System.Collections;
using UnityEngine;

public class RangedEnemy : Enemy
{

    //Type of Range enemies
    //Single shooter, multi bullet attack, spiral attack.
    [SerializeField] private GameObject _ammoPrefab;
    [SerializeField] private GameObject _firingPOS;
    [SerializeField] private float _attackCoolDown = 3f;
    private bool _canFire = true;

    protected override void Update()
    {

        base.Update();

        if (Vector3.Distance(transform.position, _target.transform.position) < _distance && _canFire == true && _isDead == false)
            Attack();


    }

    private void Attack()
    {
        Instantiate(_ammoPrefab, _firingPOS.transform.position, _firingPOS.transform.rotation);
        _anim.SetTrigger("Attack");
        _canFire = false;
        StartCoroutine(FireCooldownRoutine());
    }

    IEnumerator FireCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCoolDown);
        _canFire = true;
    }
}
