using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIBehavior : MonoBehaviour
{
    [Space]
    [Header("Behavior Variables")]
    [Range(0f,100f)]
    [SerializeField]
    private float _detectRadius;

    [Space]
    [Header("Chaches")]
    [SerializeField]
    private NavMeshAgent _agent;
    private Collider[] Hits = new Collider[2];

    [Space]
    [SerializeField]
    private Transform RunFromTransform;

    public LayerMask PlayerMask;

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
                    - (RunFromTransform.position - transform.position).normalized * 3f
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
            if (Physics.OverlapSphereNonAlloc(transform.position, _detectRadius, Hits, PlayerMask) > 0)
            {
                int index = -1;
                float lastDistance = float.MaxValue;
                for (int i = 0; i < Hits.Length; i++)
                {
                    if (Hits[i] != null)
                    if (lastDistance > (Hits[i].transform.position - transform.position).sqrMagnitude)
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
