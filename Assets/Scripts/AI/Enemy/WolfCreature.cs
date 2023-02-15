using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class WolfCreature : BehaviorTree.Tree
{
    [Header("Note: READ ME! \n \n *Tree Settings don't change in Play Time \n *Variables have tooltips")]
    [Header("Tree Settings")]
    [Space]


    [Header("Detections")]
    [Space]


    [Range(0.1f, 5f)]
    [SerializeField]
    [Tooltip("how close to ground you need to be for it to register that rock is on the ground")]
    protected float _groundCheckDistance = 2f;

    [Range(0.1f, 5f)]
    [SerializeField]
    [Tooltip("how close to ground you need to be for it to register that rock is on the ground")]
    protected float _groundCheckRadius = 0.5f;

    [SerializeField]
    [Tooltip("Which Layer the creature will check for ground on")]
    protected LayerMask _groundLayerMask = 1 << 8;
    [Space]
    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of players to avoid")]
    protected float _playerDetectRadius = 5f;

    [Range(1, 8)]
    [SerializeField]
    [Tooltip("Max amount of Players it will check for")]
    protected int _maxPlayerCount = 4;

    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    protected LayerMask _playerMask = 1 << 3;


    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of interesting object to interact with")]
    protected float _interestDetectRadius = 8f;

    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of interesting object to interact with")]
    protected float _interestGrabRadius = 2f;

    [Range(1, 30)]
    [SerializeField]
    [Tooltip("Max amount of interest it will check for")]
    protected int _maxInterestCount = 8;

    [SerializeField]
    [Tooltip("Will Move towards gameobjects with this layer")]
    protected LayerMask _interestMask = 1 << 0;

    [Header("Movement")]
    [Space]


    [Range(3f, 30f)]
    [SerializeField]
    [Tooltip("How fast the creature will be")]
    protected float _runSpeed = 6f;

    [Range(0f, 30f)]
    [SerializeField]
    [Tooltip("How far it will run after going out of range from player (0 = Disabled | will disable Run after out of range of player)")]
    protected float _runLength = 3f;

    [SerializeField]
    [Tooltip("How fast the rock will turn in angles")]
    protected float _rotationalSpeed = 100f;

    [Space]

    [Range(1f, 3f)]
    [SerializeField]
    [Tooltip("How close to destination until it consider it on the destionation")]
    protected float _stopDistance = 1f;

    [Range(1f, 10f)]
    [SerializeField]
    [Tooltip("How close it will follow to follow target")]
    protected float _followDistance = 1f;

    [Space]

    [Range(1f, 10f)]
    [SerializeField]
    [Tooltip("How far it will go each idle iterration \n(how far it could move everytime it decides to move while idle)")]
    protected float _idleWalkDistance = 3f;

    [SerializeField]
    [Tooltip("How often they move. X is min value Y is max Value \n (How Often it will decide to move after x->y Seconds)")]
    protected Vector2 _idleMovementFrequency = new Vector2(3f, 10f);

    [Header("States")]
    [Space]


    [SerializeField]
    [Tooltip("Will determen if the creature is sleeping on start")]
    protected bool _isAwakeOnStart = true;

    [Header(
        "Note:" +
        "\n Red -> Player Detection Range " +
        "\n cyan -> Interest Detection Range " +
        "\n magenta -> Interest grab Range " +
        "\n Yellow -> Walk Idle " +
        "\n Blue -> Ground Check" +
        "\n White Run Length")
    ]

    [SerializeField]
    private bool _removeGizmoOnPlay = false;
    [Space]
    [SerializeField]
    private bool _gizmoPlayerDetectRadius = false;
    [SerializeField]
    private bool _gizmoInterestDetectRadius = false;
    [SerializeField]
    private bool _gizmoIdleMovement = false;
    [SerializeField]
    private bool _GizmoGroundCheck = false;
    [SerializeField]
    private bool _gizmoRunLenght = false;

    private Rigidbody _rb;
    private Animator _animator;
    private void Awake()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody>();
        }

        if (!_animator)
        {
            if (TryGetComponent<Animator>(out _animator))
            {
                if (PebbleCreature.Debug) UnityEngine.Debug.Log($"Found Animator: {gameObject.name}");
            }
            else
            {
                UnityEngine.Debug.LogError($"No Animator On Pebble: {gameObject.name}");
            }
        }

        if (_removeGizmoOnPlay)
        {
            _gizmoPlayerDetectRadius = false;
            _gizmoInterestDetectRadius = false;
            _gizmoIdleMovement = false;
            _GizmoGroundCheck = false;
            _gizmoRunLenght = false;
        }
    }
    protected override void Start()
    {
        base.Start();

    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new AnimationNode(_animator, _rb),
            new Selector(new List<Node> {
                new CheckForGround(transform,_groundLayerMask, _groundCheckDistance, _groundCheckRadius),
                new GravityNode(_rb)
            }),
            new Dazed(),
            new CheckForGround(transform,_groundLayerMask, _groundCheckDistance, _groundCheckRadius),
            new RotateTowardsVelocity(_rb, _rotationalSpeed),
            new Selector(new List<Node>
            {
                new Selector(new List<Node>{

                    new Sequence(new List<Node>
                    {
                        new WaitForNode(4,4, "Yeet"),
                        new LiftObjectNode(transform,_interestGrabRadius),
                        new SetVariableNode("Yeet", true)
                    }),

                    new Sequence(new List<Node>
                    {
                        new HasPickedUp(),
                        new MoveAwayFromPosition(_rb, _runSpeed, _runLength, TreeVariables.PickUpArea)
                    })
                }),

                new Sequence(new List<Node> {
                    new Selector(new List<Node>{
                            new Sequence(new List<Node>
                            {
                                new CheckForPlayer(transform,_playerDetectRadius,_maxPlayerCount,_playerMask),
                                new MoveAwayFromTransform(_rb, _runSpeed, _runLength, TreeVariables.Player),
                            }),

                            new MoveToPosition(_rb,"RequestedNewPosition", _runSpeed, _stopDistance, 0.1f),

                            new Sequence(new List<Node>
                            {
                                new CheckForTransform(transform,_interestDetectRadius, _maxInterestCount, _interestMask, TreeVariables.InterestTarget),
                                new MoveTowardsTransform(_rb, _runSpeed, _runLength, TreeVariables.InterestTarget),
                                new LiftObjectNode(transform,_interestGrabRadius)
                            }),

                            new Sequence(new List<Node>
                            {
                                new WaitForNode(_idleMovementFrequency.x,_idleMovementFrequency.y, "ReachedIdlePos"),
                                new IdleMove(transform, _rb, _idleWalkDistance, _stopDistance, _runSpeed)
                            }),

                            new StopMovement(_rb)
                    }),
                })
            })
        });

        return root;
    }
    private void OnDrawGizmos()
    {

        if (_gizmoPlayerDetectRadius && _playerDetectRadius != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _playerDetectRadius);
        }

        if (_gizmoInterestDetectRadius && _interestDetectRadius != 0)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _interestDetectRadius);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _interestGrabRadius);
        }

        if (_gizmoIdleMovement && _idleWalkDistance != 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(_idleWalkDistance, 0.1f, _idleWalkDistance));
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
