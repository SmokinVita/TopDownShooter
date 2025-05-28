using UnityEngine;

public class CircuitDrops : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject[] _availableDrops;

    public int Health { get ; set; }

    public void Damage(int damageAmount)
    {
        int dropIndex = Random.Range(0, _availableDrops.Length);
        Instantiate(_availableDrops[dropIndex], transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
