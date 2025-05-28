using System.Collections;
using UnityEngine;

public class Gripper : Enemy
{
    [SerializeField] private int _maxAmountOfAttacks = 2;
    private GripperGrab _attack;
    private bool _canAttack = true;
    [SerializeField] private float _attackCoolDown = 3f;
    private bool _called50;
    private bool _called20;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _firePoint;

    protected override void Start()
    {
        base.Start();

        _attack = GetComponentInChildren<GripperGrab>();
        if (_attack == null)
            Debug.Log("GripperGrab is NULL!");
    }

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
                _attack.IsGrabbing(false);
                break;
            case 1:
                _anim.SetTrigger("Shoot");
                Shoot();
                _attack.IsGrabbing(false);
                break;
            case 2:
                _anim.SetTrigger("Spin");
                _attack.IsGrabbing(false);
                break;
            case 3:
                _anim.SetTrigger("Grab");
                _attack.IsGrabbing(true);
                break;
        }
        _canAttack = false;
        StartCoroutine(AttackCoolDownRoutine());
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firePoint.transform.position, Quaternion.identity);
    }

    //start attack cool down.
    private IEnumerator AttackCoolDownRoutine()
    {
        yield return new WaitForSeconds(_attackCoolDown);
        _canAttack = true;
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
        else if (currentHealth <= .2f && _called20 == false)
        {
            _maxAmountOfAttacks++;
            _called20 = true;
        }
    }
}
