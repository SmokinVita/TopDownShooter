using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _dmgOutput = 2;
    [SerializeField] private bool _isPlayersBullet;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        if (_isPlayersBullet)
        {
            _player = FindAnyObjectByType<Player>();
            if (_player == null)
                Debug.Log("bullet Can't find player");
        }

        DestroyObject();
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
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        if (transform.parent != null)
        {
            Destroy(this.gameObject.transform.parent.gameObject, 3f);
        }
        else
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
