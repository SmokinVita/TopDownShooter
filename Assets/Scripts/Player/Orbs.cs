using UnityEngine;


public class Orbs : MonoBehaviour
{

    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.down, _speed * Time.deltaTime);
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
