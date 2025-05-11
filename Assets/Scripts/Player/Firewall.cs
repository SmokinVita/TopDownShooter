using UnityEngine;

public class Firewall : MonoBehaviour
{

    [SerializeField] private float _cooldownHit = 3f;
    private float _timer;
    private IDamageable _damageable;

    private void OnTriggerEnter(Collider other)
    {
        _damageable = other.GetComponent<IDamageable>();
    }

    private void Update()
    {
        if (Time.time > _timer && _damageable != null)
        {
            _timer = Time.time + _cooldownHit;
            _damageable.Damage(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _damageable = null;
        }
    }
}
