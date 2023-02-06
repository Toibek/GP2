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

    [Range(1f, 5f)]
    [SerializeField]
    [Tooltip("how close to ground you need to be for it to register that rock is on the ground")]
    private float _groundCheckDistance = 2f;
    [Range(3f, 30f)]
    [SerializeField]
    [Tooltip("How far it will run after going out of range from player")]
    private float _runSpeed = 6f;
    [SerializeField]
    private float _rotationalSpeed = 100f;

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
    [Tooltip("Will determen if the pebble is sleeping on start")]
    private bool _isAwakeOnStart = true;
    [SerializeField]
    [Tooltip("How often they move. X is min value Y is max Value")]
    private Vector2 _idleMovementFrequency = new Vector2(3f,10f);
    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    private LayerMask _playerMask = 1<<3;
    [Header("Gizmos")]
    [Header("Note:\n Red -> Detection Range \n Yellow -> Walk Idle \n Blue -> Ground Check")]
    [SerializeField]
    private bool _gizmoDetectRadius;
    [SerializeField]
    private bool _gizmoIdleMovement;
    [SerializeField]
    private bool _GizmoGroundCheck;

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
        Node root = new Sequence(new List<Node>
        {
            new CheckForGround(transform),
            new IsAwake(_isAwakeOnStart, transform, _detectRadius, _maxPlayerCount, _playerMask),
            new RotateTowardsVelocity(_rb, _rotationalSpeed),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckForPlayer(transform,_detectRadius,_maxPlayerCount,_playerMask),
                    new MoveToPosition(_rb, _runSpeed)
                }),

                new Sequence(new List<Node>
                {
                    new WaitForNode(_idleMovementFrequency.x,_idleMovementFrequency.y, "ReachedIdlePos"),
                    new IdleMove(transform, _rb, _idleMovement, _stopDistance)
                })
            })
        });
        
        return root;
    }

    private void OnDrawGizmos()
    {

        if (_gizmoDetectRadius && _detectRadius != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
        }

        if (_gizmoIdleMovement && _idleMovement != 0) 
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(_idleMovement, 0.1f, _idleMovement)); 
        }

        if (_GizmoGroundCheck) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 2f);
        }
    }
}
