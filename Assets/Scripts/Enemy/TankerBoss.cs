using System.Collections;
using UnityEngine;

public class TankerBoss : Enemy
{
    [SerializeField] private float _attackCoolDown = 3f;
    private bool _canAttack = true;
    private int _maxAmountOfAttacks = 2;
    private bool _called50 = false;
    public bool _called20 = false;

    protected override void Update()
    {
        base.Update();

        //if cooldown is good attack, call attack!
        if (Vector3.Distance(transform.position, _target.transform.position) < _distance && _canAttack == true)
        {
            Attack();
        }

    }

    protected void Attack()
    {
        int attackSlot = Random.Range(0, _maxAmountOfAttacks);

        switch (attackSlot)
        {
            case 0:
                _anim.SetTrigger("Punch");
                break;
            case 1:
                _anim.SetTrigger("Slam");
                break;
            case 2:
                _anim.SetTrigger("Shoot");
                break;
            case 3:
                SpawnEnemies();
                break;
        }
        _canAttack = false;
        StartCoroutine(AttackCoolDownRoutine());
    }
    //start attack cool down.
    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(_attackCoolDown);
        _canAttack = true;
    }

    private void SpawnEnemies()
    {
        //if player is within 5m
        //Spawn mobs around player
        float distance = Vector3.Distance(gameObject.transform.position, _target.transform.forward);
        if (distance <= 5f)
        {
            _anim.SetTrigger("JumpSlam");
            //SpawnManager.Instance.SpawnEnemiesAroundPlayer();
            StartCoroutine(SpawnEnmiesDelayRoutine());
        }
    }

    IEnumerator SpawnEnmiesDelayRoutine()
    {
        yield return new WaitForSeconds(.20f);
        SpawnManager.Instance.SpawnEnemiesAroundPlayer();
    }

    public override void Damage(int damageAmount)
    {
        base.Damage(damageAmount);

        float currentHealth = (float)Health / _health;

        if (currentHealth > 0.5f)
        {
            return;
        }
        else if (currentHealth <= .5f && _called50 == false)
        {
            _maxAmountOfAttacks++;
            _called50 = true;
        }
        else if(currentHealth <=.2f && _called20 == false)
        {
            _maxAmountOfAttacks++;
            _called20 = true;
        }
    }
}
