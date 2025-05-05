
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private GameManager _gameManger;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Vector3 _spawnPoint;
    private int _spawn;

    [SerializeField] private GameObject _tanker;
    [SerializeField] private GameObject _gripper;

    [SerializeField] private GameObject _enemyHolder;

    [SerializeField] private PlayerMovement _player;

    [SerializeField] private float _TankerSpawnTimer = 90f;
    [SerializeField] private float _GripperSpawnTimer = 150f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        if (_player == null)
            Debug.Log("Can't find PLAYER!");

        _gameManger = FindObjectOfType<GameManager>();

        StartCoroutine(SpawnMonsters());

        InvokeRepeating("SpawnTanker", _TankerSpawnTimer, _TankerSpawnTimer);
        InvokeRepeating("SpawnGripper", _GripperSpawnTimer, _GripperSpawnTimer);

    }

    private IEnumerator SpawnMonsters()
    {
        while (_gameManger.IsDead() == false)
        {
            //Spawning enemy around 20m away from player's pos.
            float xSpawn = Random.Range(_player.transform.position.x - 30, _player.transform.position.x + 30);
            float zSpawn = Random.Range(_player.transform.position.z - 30, _player.transform.position.z + 30);

            //Trying to Spawn outside of camera view.
            //float xSpawn = Camera.main.pixelHeight;
            //float zSpawn = Camera.main.pixelWidth;

            _spawnPoint = new Vector3(xSpawn, 0, zSpawn);
            _spawn = Random.Range(0, _enemies.Length);
            yield return new WaitForSeconds(2f);
            GameObject enemies = Instantiate(_enemies[_spawn], _spawnPoint, Quaternion.identity);
            enemies.transform.parent = _enemyHolder.transform;
        }
    }

    private void SpawnTanker()
    {

        Instantiate(_tanker, _spawnPoint, Quaternion.identity);
    }

    private void SpawnGripper()
    {
        Instantiate(_gripper, _spawnPoint, Quaternion.identity);
    }


    public void SpawnEnemiesAroundPlayer()
    {

    }
}
