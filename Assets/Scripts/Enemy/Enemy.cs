using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _target;
    private Vector3 _destination;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.Log($"{this.gameObject.name}'s NavMeshAgent is NULL!");
        }

        _agent.destination = _destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_destination, _target.transform.position) > 1f)
        {
            _destination = _target.transform.position;
            _agent.destination = _destination;
        }
    }
}
