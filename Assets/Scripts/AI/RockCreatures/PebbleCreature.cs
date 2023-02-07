using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PebbleCreature : BehaviorTree.Tree
{
    public static bool Debug = false;
    //Tree
    [Header("Note: READ ME! \n \n *Tree Settings don't change in Play Time \n *Variables have tooltips")]
    [Header("Tree Settings")]

    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of gameobjects to avoid")]
    private float _detectRadius = 5f;

    [Range(0.1f, 5f)]
    [SerializeField]
    [Tooltip("how close to ground you need to be for it to register that rock is on the ground")]
    private float _groundCheckDistance = 2f;

    [Range(0.1f, 5f)]
    [SerializeField]
    [Tooltip("how close to ground you need to be for it to register that rock is on the ground")]
    private float _groundCheckRadius= 0.5f;

    [Range(3f, 30f)]
    [SerializeField]
    [Tooltip("How fast the rock will be")]
    private float _runSpeed = 6f;

    [Range(0f, 30f)]
    [SerializeField]
    [Tooltip("How far it will run after going out of range from player (0 = Disabled)")]
    private float _runLength = 3f;

    [SerializeField]
    [Tooltip("How fast the rock will turn in angles")]
    private float _rotationalSpeed = 100f;

    [Range(1f, 3f)]
    [SerializeField]
    [Tooltip("How close to destination until it consider it on the destionation")]
    private float _stopDistance = 1f;

    [Range(1, 8)]
    [SerializeField]
    [Tooltip("Max amount of Players it will check for")]
    private int _maxPlayerCount = 4;

    [Range(1f, 10f)]
    [SerializeField]
    [Tooltip("How far it will go each idle movement iterration \n(how far it will move everytime it decides to move while idle)")]
    private float _idleMovement = 3f;

    [SerializeField]
    [Tooltip("Will determen if the pebble is sleeping on start")]
    private bool _isAwakeOnStart = true;

    [SerializeField]
    [Tooltip("How often they move. X is min value Y is max Value \n (How Often it will decide to move after x->y Seconds)")]
    private Vector2 _idleMovementFrequency = new Vector2(3f,10f);

    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    private LayerMask _playerMask = 1<<3;

    [Header("Gizmos")]
    [Header("Note:" +
        "\n Red -> Detection Range " +
        "\n Yellow -> Walk Idle " +
        "\n Blue -> Ground Check" +
        "\n White Run Length")]
    [SerializeField]
    private bool _removeGizmoOnPlay = false;
    [Space]
    [SerializeField]
    private bool _gizmoDetectRadius = false;
    [SerializeField]
    private bool _gizmoIdleMovement  = false;
    [SerializeField]
    private bool _GizmoGroundCheck = false;
    [SerializeField]
    private bool _gizmoRunLenght = false;

    private Rigidbody _rb;
    //

    private void Awake()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (_removeGizmoOnPlay)
        {
            _gizmoDetectRadius = false;
            _gizmoIdleMovement = false;
            _GizmoGroundCheck = false;
            _gizmoRunLenght = false;
        }
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new Selector(new List<Node> {
                new CheckForGround(transform, _groundCheckDistance, _groundCheckRadius),
                new GravityNode(_rb)
            }),
            
            new CheckForGround(transform, _groundCheckDistance, _groundCheckRadius),
            new IsAwake(_isAwakeOnStart, transform, _detectRadius, _maxPlayerCount, _playerMask),
            new RotateTowardsVelocity(_rb, _rotationalSpeed),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node>
                {
                    new CheckForPlayer(transform,_detectRadius,_maxPlayerCount,_playerMask),
                    new MoveAwayFromPosition(_rb, _runSpeed, _runLength)
                }),

                new MoveToPosition(_rb,"RequestedNewPosition", _runSpeed, _stopDistance, 0.1f),

                new Sequence(new List<Node>
                {
                    new WaitForNode(_idleMovementFrequency.x,_idleMovementFrequency.y, "ReachedIdlePos"),
                    new IdleMove(transform, _rb, _idleMovement, _stopDistance, _runSpeed)
                }),

                new StopMovement(_rb),
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
            Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
            Gizmos.DrawWireSphere(transform.position + Vector3.down * _groundCheckDistance, _groundCheckRadius);
        }

        if (_gizmoRunLenght && _runLength != 0)
        {
            Gizmos.color = Color.white; 
            Gizmos.DrawWireSphere(transform.position, _runLength);

        }
    }
}
