using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIBehavior : MonoBehaviour
{
    [Space]
    [Header("Behavior Variables")]

    [Range(0f,100f)]
    [SerializeField]
    [Tooltip("Detection range of gameobjects to avoid")]
    private float _detectRadius = 5f;
    
    [Range(0f, 20f)]
    [SerializeField]
    [Tooltip("How far from player it will try to run")]
    private float _runLength = 3f;

    [Range(1f, 3f)]
    [SerializeField]
    [Tooltip("How close to destination untill it consider it on the destionation")]
    private float _stopDistance = 1f;

    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    private LayerMask _playerMask;

    [Space]
    [Header("Chaches")]
    private NavMeshAgent _agent;
    private Collider[] Hits = new Collider[4];

    private Transform RunFromTransform;


    [ContextMenu("TestMovement")]
    public void TestMove()
    {
        Debug.LogWarning("Running Editor Script");
        if (!_agent) return;

        _agent.SetDestination(RunFromTransform.position);
    }

    private void Awake()
    {
        if (!_agent)
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        Hits = new Collider[2];

        if (_agent) _agent.stoppingDistance = _stopDistance;
    }

    private void Update()
    {
        if (!_agent) return;
        

        //
        // Moves AI Away from Transform (players)
        //

        if (RunFromTransform)
        {
            _agent.SetDestination(
                    transform.position
                    - (RunFromTransform.position - transform.position).normalized * _runLength
                    );
            //if (NavMesh.Raycast(transform.position, RunFromTransform.position, out NavMeshHit hit, NavMesh.AllAreas))
            //{
            //    _agent.SetDestination(
            //                transform.position
            //                - (hit.position - transform.position).normalized * 3f
            //     );
            //}
            //else
            //{
            //    _agent.SetDestination(
            //        transform.position
            //        - (RunFromTransform.position - transform.position).normalized * 3f
            //        );
            //}

        }

        //
        // Finds Nearest Player 
        //

        if (_detectRadius != 0)
        {
            if (Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, Hits, _playerMask) > 0)
            {
                int index = -1;
                float lastDistance = float.MaxValue;
                for (int i = 0; i < Hits.Length; i++)
                {
                    if (Hits[i] != null && lastDistance > (Hits[i].transform.position - transform.position).sqrMagnitude)
                    {
                        lastDistance = (Hits[i].transform.position - transform.position).sqrMagnitude;
                        index = i;
                    }
                }
                RunFromTransform = Hits[index].transform;
            }
            else
            {
                RunFromTransform = null;
            }
        }

        //
        //
        //
    }

    private void OnDrawGizmosSelected()
    {
        if (_detectRadius != 0) Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }

    public void ChangeState(State newState)
    {
        //_currentState = newState;
    }

    public enum State
    {
        notAssigned = -1,
        idle,
        walk,
        runs
    }
}
