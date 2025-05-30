using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [Header("Firing")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePos;
    private bool _canShoot = true;
    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private AudioClip _basicSFX, _circleShotSFX, _tripleShotSFX;
    private int _shootingIndex = 0;

    [Header("OrbUpgrade")]
    [SerializeField] private GameObject[] _orbUpgrade;
    [SerializeField] private int _orbUpgradeIndex = 0;

    [Header("AttackUpgrades")]
    [SerializeField] private GameObject _singleShot;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private GameObject _circleShot;
    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private int _maxAttackSpdUpgrades = 3;
    [SerializeField] private int _currentAttackSpdUpgrades;
    [SerializeField] private int _maxSpdUpgrades = 5;
    [SerializeField] private int _currentSpdUpgrades;
    [SerializeField] private int _maxbulletStrUpgrades = 4;
    [SerializeField] private int _currentBulletStrUpgrades;
    [SerializeField] private int _bulletStrength = 2;

    [Header("FirewallUpgrade")]
    [SerializeField] private GameObject[] _fireWalls;
    [SerializeField] private int _fireWallsIndex = 0;

    [SerializeField] private GameObject _ammoHolder;


    public int Health { get; set; }
    [SerializeField] private int _health;
    private bool _isDead = false;

    private PlayerMovement _movement;
    private PlayerAnimation _playerAnim;
    private GameManager _gameManager;
    private PauseMenuUI _pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("GameManager is NULL!");
        }

        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        if (_playerAnim == null)
            Debug.Log("Player thinks player animation is NULL!");

        _pauseMenuUI = FindObjectOfType<PauseMenuUI>();
        if (_pauseMenuUI == null)
            Debug.Log("Can't Find PauseMenuUI");

        _cameraShake = FindObjectOfType<CameraShake>();
        if (_cameraShake == null)
            Debug.Log("CameraShake is NULL!");

        Health = _health;
        UIManager.Instance.UpdateMaxHealth(Health);
        UIManager.Instance.HealthBar(Health);

        _movement = GetComponent<PlayerMovement>();

        AudioManager.Instance.SetSFX(_basicSFX);
    }

    private void Update()
    {

        if (!_isDead || _gameManager.IsGameActive() == true)
        {
            Shoot();
        }
    }

    //player shotgun shot(triple shot)- done, Machinegun fire, Big Shot but slow
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot == true)
        {
            _playerAnim.ShootAnim(_shootingIndex);
            AudioManager.Instance.PlaySFX();
            
            _cameraShake.CallForShake();
            _canShoot = false;
            Instantiate(_projectilePrefab, _firePos.transform.position, _firePos.transform.rotation);
            StartCoroutine(ShootCoolDownRoutine());
        }
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
        UIManager.Instance.HealthBar(Health);


        if (Health <= 0)
        {
            _isDead = true;
            _gameManager.PlayerHasDied(_isDead);
            _playerAnim.DeathAnimation();
            //maybe use shader to fade character away
            Destroy(gameObject, 2f);
        }
    }

    public void AddUpgrade(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Orbs":
                OrbActivate();
                break;
            case "Attack Speed":
                AttackSpeedUpgade();
                break;
            case "Firewall":
                FirewallActivate();
                break;
            case "Speed":
                SpeedIncrease();
                break;
            case "TripleShot":
                TripleShot();
                break;
            case "CircleShot":
                CirlceShot();
                break;
            case "BulletStr":
                BulletStrUpgrade();
                break;
            case "Heal":
                Heal();
                break;
               
        }

        UIManager.Instance.RemoveListeners();
    }

    private void OrbActivate()
    {
        _orbUpgradeIndex++;
        _pauseMenuUI.UpdateUpgradeLvl("Orbs",_orbUpgradeIndex);
        if (_orbUpgradeIndex >= _orbUpgrade.Length)
        {
            UpgradeHandler.Instance.MaxedOutSkill("Orbs");
            return;
        }
        _orbUpgrade[_orbUpgradeIndex - 1].SetActive(true);
    }

    private void AttackSpeedUpgade()
    {
        _currentAttackSpdUpgrades++;
        _pauseMenuUI.UpdateUpgradeLvl("Attack", _currentAttackSpdUpgrades);
        if (_currentAttackSpdUpgrades >= _maxAttackSpdUpgrades)
        {
            UpgradeHandler.Instance.MaxedOutSkill("Attack");
            return;
        }
        _shootCooldown -= .2f;
    }

    private void BulletStrUpgrade()
    {
        _currentBulletStrUpgrades++;
        _pauseMenuUI.UpdateUpgradeLvl("BulletStr", _currentBulletStrUpgrades);
        if(_currentBulletStrUpgrades >= _maxbulletStrUpgrades)
        {
            UpgradeHandler.Instance.MaxedOutSkill("BulletStr");
            return;
        }
        _bulletStrength += _currentBulletStrUpgrades;

    }

    public int BulletStr()
    {
        return _bulletStrength;
    }

    private void FirewallActivate()
    {
        _fireWalls[_fireWallsIndex].SetActive(true);
        _fireWallsIndex++;
        _pauseMenuUI.UpdateUpgradeLvl("Firewall", _fireWallsIndex);
        if(_fireWallsIndex >= _fireWalls.Length)
        {
            UpgradeHandler.Instance.MaxedOutSkill("Firewall");
            return;
        }
    }

    private void SpeedIncrease()
    {
        _currentSpdUpgrades++;
        _pauseMenuUI.UpdateUpgradeLvl("Speed", _currentSpdUpgrades);
        if (_currentSpdUpgrades >= _maxSpdUpgrades)
        {
            UpgradeHandler.Instance.MaxedOutSkill("Speed");
            return;
        }
       _movement.IncreaseSpd();
    }

    private void TripleShot()
    {
        _shootingIndex = 3;
        AudioManager.Instance.SetSFX(_tripleShotSFX);
        _projectilePrefab = _tripleShot;
    }

    private void CirlceShot()
    {
        _shootingIndex = 2;
        AudioManager.Instance.SetSFX(_circleShotSFX);
        _projectilePrefab = _circleShot;
    }

    private IEnumerator ShootCoolDownRoutine()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }

    public void Heal()
    {
        int healAmount = Random.Range(20, 51);
        Health += healAmount;
        if (Health > _health)
        {
            Health = _health;
        }

        UIManager.Instance.HealthBar(Health);
    }
}
