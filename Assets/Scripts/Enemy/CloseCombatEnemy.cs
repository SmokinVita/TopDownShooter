using System.Collections;
using UnityEngine;

public class CloseCombatEnemy : Enemy
{

    [SerializeField] private float _attackCoolDown = 3f;
    [SerializeField] private Collider _attackCollider;
    private bool _canAttack = true;


    protected override void Update()
    {
        if (_gameManager.IsDead() || _isDead == true)
        {
            _agent.isStopped = true;
            return;
        }


        base.Update();


         if (Vector3.Distance(transform.position, _target.transform.position) < _distance && _canAttack == true)
             Attack();
     }

    private void Attack()
    {
        _attackCollider.enabled = true;
        _anim.SetTrigger("Attack");
        
        _canAttack = false;
        
        StartCoroutine(AttackCooldownRoutine());
        _attackCollider.enabled = false;

        Debug.Log("Attacking!");
    }

    IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCoolDown);
        _canAttack = true;
    }
}
