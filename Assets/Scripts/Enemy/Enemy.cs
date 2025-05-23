using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    protected GameManager _gameManager;

    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected PlayerMovement _target;
    [SerializeField] protected GameObject _lootPrefab;
    [SerializeField] protected float _distance = 1f;
    [SerializeField] protected bool _isDead = false;
    [SerializeField] private Collider _collider;

    protected Animator _anim;
    protected Vector3 _destination;

    [SerializeField] protected int _health = 5;

    public int Health { get; set; }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.Log($"{this.gameObject.name}'s NavMeshAgent is NULL!");
        }

        _target = FindObjectOfType<PlayerMovement>();
        if (_target == null)
        {
            Debug.Log("Can't find Player!");
        }

        _anim = GetComponentInChildren<Animator>();
        if (_anim == null)
        {
            Debug.Log($"Can't find {name}'s animator!");
        }

        _collider = GetComponentInChildren<Collider>();
        if (_collider == null)
            Debug.Log($"{name} has no collider!");

        _agent.destination = _destination;

        SetHealth();
    }

    protected virtual void Update()
    {
        if (_gameManager.IsDead() || _isDead == true)
        {
            _agent.isStopped = true;
            return;
        }
        
        Movement();
    }

    private void SetHealth()
    {
        switch (_gameManager.Setting())
        {
            case GameManager.Difficulty.Easy:
                Health = _health / 2;
                break;
            case GameManager.Difficulty.Medium:
                Health = _health;
                break;
            case GameManager.Difficulty.Hard:
                Health = _health * 2;
                break;
        }
        _health = Health;
    }

    protected virtual void Movement()
    {

        float currentDistance = Vector3.Distance(transform.position, _target.transform.position);
        if (currentDistance > _distance)
        {
            _agent.destination = _target.transform.position;
        }

        float velocity = _agent.velocity.magnitude / _agent.speed;
        _anim.SetFloat("Speed", velocity);
    }

    public virtual void Damage(int damageAmount)
    {
        

        Health -= damageAmount;
        if (_anim == null)
            return;

        _anim.SetTrigger("Hit");
        if (Health <= 0 && _isDead != true)
        {
            _collider.enabled = false;
            Instantiate(_lootPrefab, new Vector3(transform.position.x, 1.5f, transform.position.z), Quaternion.identity);
            _isDead = true;
            _anim.SetBool("HasDied", true);
            _anim.SetTrigger("Died");
            Destroy(gameObject, 5f);
        }
    }
}
