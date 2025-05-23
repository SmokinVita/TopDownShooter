using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{

    [SerializeField] private GameObject[] _wayPoints;
    [SerializeField] private GameObject _currentDestination;
    private NavMeshAgent _agent;
    private bool _canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentDestination = _wayPoints[Random.Range(0, _wayPoints.Length)];
        _agent.destination = _currentDestination.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if at currentDestination && canmove is true
        //set new destion
        float distance = Vector3.Distance(transform.position, _currentDestination.transform.position);
        if (distance < 2f)
        {
            _canMove = false;
            _currentDestination = _wayPoints[Random.Range(0, _wayPoints.Length)];
            StartCoroutine(WaitRoutine());
            
        }

        if (_canMove == true)
        {
            _agent.destination = _currentDestination.transform.position;
        }
    }

    IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(5f);
        _canMove = true;
    }
}
