
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


    [Header("Tanker Spawn Info")]
    [SerializeField] private int _amoutOfEnemiesToSpawn = 10;
    [SerializeField] private GameObject _enemyToSpawnPrefab;
    [SerializeField] private float radius = 10f;


    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
        if (_player == null)
            Debug.Log("Can't find PLAYER!");

        _gameManger = FindObjectOfType<GameManager>();
        if (_gameManger == null)
            Debug.Log("SpawnManager can't locate GameManager is NULL!");
        

        InvokeRepeating("SpawnTanker", _TankerSpawnTimer, _TankerSpawnTimer);
        InvokeRepeating("SpawnGripper", _GripperSpawnTimer, _GripperSpawnTimer);

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (_gameManger.IsDead() == false)
        {
            //Spawning enemy around 20m away from player's pos.
            float xSpawn = Random.Range(_player.transform.position.x - 30, _player.transform.position.x + 30);
            float zSpawn = Random.Range(_player.transform.position.z - 30, _player.transform.position.z + 30);

            //add 4 spawnpoints on player and randomly pick on to spawn at.

            _spawnPoint = new Vector3(xSpawn, 0, zSpawn);
            _spawn = Random.Range(0, _enemies.Length);
            yield return new WaitForSeconds(2f);
            GameObject enemies = Instantiate(_enemies[_spawn], _spawnPoint, Quaternion.identity);
            enemies.transform.parent = _enemyHolder.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SpawnGripper();
        }
    }

    private void SpawnTanker()
    {
        GameObject boss = Instantiate(_tanker, _spawnPoint, Quaternion.identity);
        boss.transform.parent = _enemyHolder.transform;
    }

    private void SpawnGripper()
    {
        GameObject boss = Instantiate(_gripper, _spawnPoint, Quaternion.identity);
        boss.transform.parent = _enemyHolder.transform;
    }


    public void SpawnEnemiesAroundPlayer()
    {
        float angle = 360f / _amoutOfEnemiesToSpawn;

        for (int i = 0; i < _amoutOfEnemiesToSpawn; i++)
        {
            float x = _player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad * i);
            float z = _player.transform.position.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad * i);
            Vector3 spawnPoint = new Vector3(x, 0, z);

            Instantiate(_enemyToSpawnPrefab, spawnPoint, Quaternion.identity);
        }
    }
}
