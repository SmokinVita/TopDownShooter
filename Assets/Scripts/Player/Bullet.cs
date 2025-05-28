using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _dmgOutput = 2;
    [SerializeField] private bool _isPlayersBullet;
    private Player _player;
    private bool _hitSomething;
    [SerializeField] private GameObject _muzzlePrefab;
    [SerializeField] private GameObject _hitPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (_isPlayersBullet)
        {
            _player = FindAnyObjectByType<Player>();
            if (_player == null)
                Debug.Log("bullet Can't find player");
        }

        if (_muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(_muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.position;
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        Invoke("DestroyObject", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null )
        {
            if (_isPlayersBullet)
            {
                _dmgOutput = _player.BulletStr();
            }
            damageable.Damage(_dmgOutput);
            GameObject hitfab = Instantiate(_hitPrefab, other.transform.position, Quaternion.identity);
            hitfab.transform.parent = other.transform;
            Destroy(gameObject);
        }
    }

    private void DestroyObject()
    {
        if (transform.parent != null)
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
