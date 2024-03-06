
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Vector3 _spawnPoint;
    private int _spawn;

    [SerializeField] private PlayerMovement _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        if (_player == null)
            Debug.Log("Can't find PLAYER!");
        
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (GameManager.Instance.IsDead() == false)
        {
            //Spawning enemy around 20m away from player's pos.
            float xSpawn = Random.Range(_player.transform.position.x - 20, _player.transform.position.x + 20);
            float zSpawn = Random.Range(_player.transform.position.z - 20, _player.transform.position.z + 20);

            //Trying to Spawn outside of camera view.
            //float xSpawn = Camera.main.pixelHeight;
            //float zSpawn = Camera.main.pixelWidth;

            _spawnPoint = new Vector3(xSpawn, 0,  zSpawn); ;
            _spawn = Random.Range(0, _enemies.Length);
            yield return new WaitForSeconds(2f);
            Instantiate(_enemies[_spawn], _spawnPoint, Quaternion.identity);
        }
    }
}
