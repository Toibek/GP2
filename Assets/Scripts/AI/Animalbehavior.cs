using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody),typeof(NavMeshAgent))]
public class Animalbehavior : MonoBehaviour
{
    [Space]
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private State _currentState;

#if UNITY_EDITOR
    [Header("Debug Only")]
    [SerializeField]
    private Transform _transform;

    [ContextMenu("TestMovement")]
    public void TestMove()
    {
        if (!_agent) return;
        _agent.SetDestination(_transform.position);
    }

#endif

    private void Awake()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (!_agent)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        if (!_agent) return;
        _agent.SetDestination(_transform.position);
    }

    public void ChangeState(State newState)
    {
        _currentState = newState;
    }

    public enum State
    {
        notAssigned = -1,
        idle,
        walk,
        runs
    }
}
