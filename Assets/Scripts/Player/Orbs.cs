using UnityEngine;


public class Orbs : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;

    private void Update()
    {

        //Vector3 eulers = transform.eulerAngles;
        //transform.localEulerAngles = new Vector3(0, eulers.y, 0);
        //transform.localRotation = Quaternion.Euler(new Vector3(0, eulers.y, 0));
        
        transform.RotateAround(_target.transform.position, Vector3.down, _speed * Time.deltaTime);
        //transform.Rotate(Vector3.up, _speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(1);
        }
    }
}
