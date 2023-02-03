using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PebbleCreature : BehaviorTree.Tree
{
    //Tree
    [Header("Note: Tree Settings don't change in Play Time")]
    [Header("Tree Settings")]
    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of gameobjects to avoid")]
    private float _detectRadius = 5f;

    [Range(0f, 20f)]
    [SerializeField]
    [Tooltip("How far it will run after going out of range from player")]
    private float _runLength = 3f;

    [Range(1f, 3f)]
    [SerializeField]
    [Tooltip("How close to destination untill it consider it on the destionation")]
    private float _stopDistance = 1f;

    [Range(1, 8)]
    [SerializeField]
    [Tooltip("Max amount of Players it will check for")]
    private int _maxPlayerCount = 4;

    [Range(1f, 10f)]
    [SerializeField]
    [Tooltip("How close to destination untill it consider it on the destionation")]
    private float _idleMovement = 3f;
    [SerializeField]
    [Tooltip("How often they move. X is min value Y is max Value")]
    private Vector2 _idleMovementFrequency = new Vector2(3f,10f);
    [SerializeField]
    private bool _gizmoDetectRadius;
    [SerializeField]
    private bool _gizmoIdleMovement;



    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    private LayerMask _playerMask = 1<<3;
    
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    //

    private void Awake()
    {

        if (!_agent)
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }
        if (_agent) _agent.stoppingDistance = _stopDistance;
    }

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new RotateTowardsVelocity(_rb, 100f),
            new Sequence(new List<Node>
            {
                new CheckForPlayer(transform,_detectRadius,_maxPlayerCount,_playerMask),
                new MoveToPosition(_rb, _runLength)
            }),

            new Sequence(new List<Node>
            {
                new WaitForNode(_idleMovementFrequency.x,_idleMovementFrequency.y)
                //new IdleMove(transform, _agent,_idleMovement)
            })
        });
        return root;
    }
    private void OnDrawGizmosSelected()
    {

        if (_gizmoDetectRadius && _detectRadius != 0) Gizmos.DrawWireSphere(transform.position, _detectRadius);
        if (_gizmoIdleMovement && _idleMovement != 0) Gizmos.DrawWireCube(transform.position, new Vector3(_idleMovement,0.1f, _idleMovement));
    }
}
