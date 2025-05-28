
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    private GameManager _gameManger;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _spawnPoints;
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

    [Header("Circuit Board Items!")]
    [SerializeField] private GameObject[] _circuitComponets;


    private void Start()
    {
        SpawnRandomCircuitComponents();

        _player = FindObjectOfType<PlayerMovement>();
        if (_player == null)
            Debug.Log("Can't find PLAYER!");

        _gameManger = FindObjectOfType<GameManager>();
        if (_gameManger == null)
            Debug.Log("SpawnManager can't locate GameManager is NULL!");
        

        InvokeRepeating("SpawnTanker", _TankerSpawnTimer, _TankerSpawnTimer);
        InvokeRepeating("SpawnGripper", _GripperSpawnTimer, _GripperSpawnTimer);
        Invoke("StartSpawning", 2f);

    }

    private void SpawnRandomCircuitComponents()
    {
        for (int i = 0; i < 6; i++)
        {
            var pos = new Vector3(Random.Range(-130, 120), -1.09f, Random.Range(-190, 60));
            Instantiate(_circuitComponets[Random.Range(0, _circuitComponets.Length)], pos, transform.rotation = new Quaternion(transform.rotation.x, Random.Range(0, 360), transform.rotation.z, transform.rotation.w));
        }
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (_gameManger.IsDead() == false)
        {
            int randomSpawnPoint = Random.Range(0, _spawnPoints.Length);
            _spawn = Random.Range(0, _enemies.Length);
            yield return new WaitForSeconds(2f);
            GameObject enemies = Instantiate(_enemies[_spawn], _spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
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
