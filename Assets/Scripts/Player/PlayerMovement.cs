using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header ("Movement")]
    [SerializeField] private float _speed = 5f;
    private Rigidbody _rb;
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_rb == null )
        {
            Debug.Log("Rigidbody is Null on Player");
        }
        _mainCamera = FindObjectOfType<Camera>();

    }

    void FixedUpdate()
    {
        if(!GameManager.Instance.IsDead())
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

}
