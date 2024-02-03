using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected PlayerMovement _target;
    [SerializeField] protected GameObject _lootPrefab;
    [SerializeField] protected float _distance = 1f;
    protected Vector3 _destination;

    [SerializeField] private int _health = 5;

    public int Health { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.Log($"{this.gameObject.name}'s NavMeshAgent is NULL!");
        }

        _target = FindObjectOfType<PlayerMovement>();
        if(_target == null)
        {
            Debug.Log("Can't find Player!");
        }

        _agent.destination = _destination;

        Health = _health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (GameManager.Instance.IsDead())
            return;

        if (Vector3.Distance(_destination, _target.transform.position) > _distance)
        {
            _destination = _target.transform.position;
            _agent.destination = _destination;
        }
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        if (Health <= 0)
        {
            Instantiate(_lootPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
