using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header ("Movement")]
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;
    private Camera _mainCamera;
    [SerializeField] private Transform _playerCharacter;

    private GameManager _gameManager;
    private PlayerAnimation _playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null )
        {
            Debug.Log("Rigidbody is Null on Player");
        }
        _mainCamera = FindObjectOfType<Camera>();

        _gameManager = FindObjectOfType<GameManager>();

        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        if (_playerAnim == null)
            Debug.Log("Player Movement can't find Player Animation");
        

    }

    void FixedUpdate()
    {
        if(!_gameManager.IsDead())
            Movement();
    }

    private void Movement()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;


        //rotates player
        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Debug.DrawRay(ray.origin, pointToLook, Color.green);

            Vector3 eulers = transform.eulerAngles;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            _playerCharacter.LookAt(new Vector3(pointToLook.x, _playerCharacter.position.y, pointToLook.z));

        }

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        movement *= _speed * Time.fixedDeltaTime;
        var skewedInput = matrix.MultiplyPoint3x4(movement);
        


        //Trying to move up and down in referance to scene.
        _rb.MovePosition(transform.position + skewedInput);
        float currentSpeed = _rb.velocity.magnitude;
        _playerAnim.RunAnimation(vertical);
    }

    public void IncreaseSpd()
    {
        _speed += .3f;
    }

}
