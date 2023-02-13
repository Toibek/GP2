using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

[RequireComponent(typeof(Rigidbody))]
public class PebbleCreature : BehaviorTree.Tree
{
    public static bool Debug = false;
    public static List<PebbleCreature> creatures = new List<PebbleCreature>();
    //Tree
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
    protected float _groundCheckRadius= 0.5f;

    [SerializeField]
    [Tooltip("Which Layer the rock will check for ground on")]
    protected LayerMask _groundLayerMask = 1<<0;
    [Space]
    [Range(0f, 100f)]
    [SerializeField]
    [Tooltip("Detection range of gameobjects to avoid")]
    protected float _detectRadius = 5f;

    [Range(1, 8)]
    [SerializeField]
    [Tooltip("Max amount of Players it will check for")]
    protected int _maxPlayerCount = 4;

    [SerializeField]
    [Tooltip("Will Move away from gameobjects with this layer")]
    protected LayerMask _playerMask = 1<<3;


    [Header("Movement")]
    [Space]


    [Range(3f, 30f)]
    [SerializeField]
    [Tooltip("How fast the rock will be")]
    protected float _runSpeed = 6f;

    [Range(0f, 30f)]
    [SerializeField]
    [Tooltip("How far it will run after going out of range from player (0 = Disabled)")]
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
    [Tooltip("How close rock will follow to follow target")]
    protected float _followDistance = 1f;

    [Space]

    [Range(1f, 10f)]
    [SerializeField]
    [Tooltip("How far it will go each idle iterration \n(how far it could move everytime it decides to move while idle)")]
    protected float _idleWalkDistance = 3f;

    [SerializeField]
    [Tooltip("How often they move. X is min value Y is max Value \n (How Often it will decide to move after x->y Seconds)")]
    protected Vector2 _idleMovementFrequency = new Vector2(3f,10f);

    [Header("States")]
    [Space]


    [SerializeField]
    [Tooltip("Will determen if the pebble is sleeping on start")]
    protected bool _isAwakeOnStart = true;

    [SerializeField]
    [Tooltip("Will determen if the pebble is Tamed on start")]
    protected bool _isTamedOnStart = false;


    [Header("Gizmos")]
   
    [Header(
        "Note:" +
        "\n Red -> Detection Range " +
        "\n Yellow -> Walk Idle " +
        "\n Blue -> Ground Check" +
        "\n White Run Length")
    ]

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
    private Animator _animator;
    [Header("Debuging")]
    [SerializeField]
    private Transform test;
    //

    public bool IsTamed => _root.GetData(TreeVariables.Tamed) != null ? (bool)_root.GetData(TreeVariables.Tamed) : _isTamedOnStart;

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
                if (Debug) UnityEngine.Debug.Log($"Found Animator: {gameObject.name}");
            }
            else
            {
                UnityEngine.Debug.LogError($"No Animator On Pebble: {gameObject.name}");
            }
        }

        if (_removeGizmoOnPlay)
        {
            _gizmoDetectRadius = false;
            _gizmoIdleMovement = false;
            _GizmoGroundCheck = false;
            _gizmoRunLenght = false;
        }

        creatures.Add(this);
    }

    [ContextMenu("Set Tamed True")]
    internal void SetTamedTrue()
    {
        _root.SetData(TreeVariables.FollowTransform, test);
        _root.SetData(TreeVariables.Tamed, true);
    }

    [ContextMenu("Set Tamed False")]
    internal void SetTamedFalse()
    {
        _root.SetData(TreeVariables.Tamed, false);
    }

    public void SetTamedState(bool isTamed, Transform followTarget = null)
    {
        _root.SetData(TreeVariables.Tamed, isTamed);
        if (followTarget != null)
            _root.SetData(TreeVariables.FollowTransform, followTarget);
    }

    public void SetFollowTarget(Transform followTarget)
    {
        _root.SetData(TreeVariables.FollowTransform, followTarget);
    }

    public void SetAwakeState(bool isAwake)
    {
        _root.SetData(TreeVariables.IsAwake, isAwake);
    }

    public void SetNewFollow(Transform target)
    {
        throw new System.Exception("not implimented 'SetNewFollow' yet");
    }

    public void Stun(float seconds)
    {
        _root.SetData(TreeVariables.Dazed, seconds);
    }

    public void DeleteThisInstance()
    {
        PebbleCreature.creatures.Remove(this);
        Destroy(gameObject);
    }

    public static void DeleteAllPebbleCreatures()
    {
        if (creatures.Count == 0) return;

        for (int i = creatures.Count - 1; i >= 0; i--)
        {
            creatures[i].DeleteThisInstance();
        }

    }

    public static void ChangeFollowTargetOnTamed(Transform newTarget)
    {
        if (creatures.Count == 0) return;

        for(int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i].IsTamed)
                creatures[i].SetNewFollow(newTarget);
        }
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
            new IsAwake(_isAwakeOnStart),
            new RotateTowardsVelocity(_rb, _rotationalSpeed),
            new Selector(new List<Node>
            {
                new Sequence(new List<Node> {
                    new IsTamed(_isTamedOnStart),
                    new Selector(new List<Node>
                    {
                            new MoveTowardsTransform(_rb, _runSpeed, _stopDistance, TreeVariables.FollowTransform),

                            new MoveToPosition(_rb,"RequestedNewPosition", _runSpeed, _stopDistance, 0.1f),

                            new Sequence(new List<Node>
                            {
                                new WaitForNode(_idleMovementFrequency.x,_idleMovementFrequency.y, "ReachedIdlePos"),
                                new IdleMove(transform, _rb, _idleWalkDistance, _stopDistance, _runSpeed)
                            }),

                            new StopMovement(_rb)
                    }),
                }),

                new Sequence(new List<Node> {
                    new IsNotTamed(_isTamedOnStart),
                    new Selector(new List<Node>{
                            new Sequence(new List<Node>
                            {
                                new CheckForPlayer(transform,_detectRadius,_maxPlayerCount,_playerMask),
                                new MoveAwayFromPosition(_rb, _runSpeed, _runLength, "Player"),
                            }),

                            new MoveToPosition(_rb,"RequestedNewPosition", _runSpeed, _stopDistance, 0.1f),

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

        if (_gizmoDetectRadius && _detectRadius != 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
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
