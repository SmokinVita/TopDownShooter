using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDamageable
{

    [Header ("Movement")]
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;
    private Camera _mainCamera;
    

    [Header("Firing")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePos;

    public int Health { get; set; }
    [SerializeField] private int _health;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null )
        {
            Debug.Log("Rigidbody is Null on Player");
        }
        _mainCamera = FindObjectOfType<Camera>();

        Health = _health;
    }

    private void Update()
    {
        if(!_isDead)
            Shoot();
    }

    void FixedUpdate()
    {
        if(!_isDead)
            Movement();
    }

    private void Movement()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Debug.DrawRay(ray.origin, pointToLook, Color.green);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement *= _speed * Time.fixedDeltaTime;

        Vector3 offset = transform.rotation * movement;
        _rb.MovePosition(transform.position + offset);
    }

    //player shotgun shot(triple shot)- done, Machinegun fire, Big Shot but slow

    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shooting!!");
            Instantiate(_projectilePrefab, _firePos.transform.position, _firePos.transform.rotation);
        }
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("I'm Hit!");
        Health-= damageAmount;

        
        if (Health <= 0)
        {
            Debug.Log("I'm Dead!!");
            //message gamemanager I am dead to stop them from attacking!
            _isDead = true;
            GameManager.Instance.PlayerHasDied(_isDead);
            //maybe use shader to fade character away
            Destroy(gameObject, 2f);
        }
    }
}
