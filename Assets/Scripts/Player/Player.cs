using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [Header("Firing")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePos;
    private bool _canShoot = true;

    [Header("OrbUpgrade")]
    [SerializeField] private GameObject[] _orbUpgrade;
    private int _orbUpgradeIndex = 0;

    public int Health { get; set; }
    [SerializeField] private int _health;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = _health;
        UIManager.Instance.UpdateMaxHealth(Health);
        UIManager.Instance.HealthBar(Health);
    }

    private void Update()
    {
        if (!_isDead)
            Shoot();
    }

    //player shotgun shot(triple shot)- done, Machinegun fire, Big Shot but slow
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot == true)
        {
            Debug.Log("Shooting!!");
            Instantiate(_projectilePrefab, _firePos.transform.position, _firePos.transform.rotation);
            _canShoot = false;
            StartCoroutine(ShootCoolDownRoutine());
        }
    }

    public void Damage(int damageAmount)
    {
        Debug.Log("I'm Hit!");
        Health -= damageAmount;
        UIManager.Instance.HealthBar(Health);


        if (Health <= 0)
        {
            _isDead = true;
            GameManager.Instance.PlayerHasDied(_isDead);
            //maybe use shader to fade character away
            Destroy(gameObject, 2f);
        }
    }

    public void AddUpgrade(string upgradeName)
    {
        Debug.Log("We selected " + upgradeName);
        //call the correct method to unlock upgrade
        //switch
        switch (upgradeName)
        {
            case "Orbs":
                OrbActivate();
                break;
            case "Attack":
                AttackUpgade();
                break;
            case "Barrier":
                BarrierActivate();
                break;

        }

        UIManager.Instance.RemoveListeners();
    }

    private void OrbActivate()
    {
        Debug.Log("Orbs Activated!!");
        _orbUpgradeIndex++;
        Debug.Log("Orb is at " + _orbUpgradeIndex.ToString());
        if (_orbUpgradeIndex >= _orbUpgrade.Length)
        {
            return;
        }
        _orbUpgrade[_orbUpgradeIndex - 1].SetActive(true);
        return;
    }

    private void AttackUpgade()
    {
        Debug.Log("AttackActivated!");
    }

    private void BarrierActivate()
    {
        Debug.Log("Barrier Activated!");
    }

    private IEnumerator ShootCoolDownRoutine()
    {
        yield return new WaitForSeconds(1);
        _canShoot = true;
    }
}
