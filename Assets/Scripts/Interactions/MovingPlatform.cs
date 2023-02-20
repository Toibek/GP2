using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class MovingPlatform : MonoBehaviour
{
    [Header("Note: \n" +
        "Set Colliders on children\n\n" +
        "Objets that should move with this platform \n should have the 'WalkOnPlatform' script \n\n" +
        "DON'T CHANGE SCALE ON THIS GAMEOBJECT!!!")]
    [Space]
    [Tooltip("How big the area the platform can move in")]
    [SerializeField]
    private Vector3 _moveVector = Vector3.one;
    [Tooltip("Where on the area it will start")]
    [SerializeField]
    [Range(0, 1)]
    private float _lerpValue = 0;
    private Vector3 _worldStartPos;
    private Quaternion _startRot;

    /// <summary>
    /// Procentage tp end Position of platform
    /// | 0 -> 1 | start -> end |
    /// </summary>
    public float LerpValue { get { return _lerpValue; } set { _lerpValue = value; } }

    private Vector3 GetRunTimePosition => Vector3.Lerp(_worldStartPos, _worldStartPos + _startRot * _moveVector, _lerpValue);
    private Vector3 GetInspectorTimePosition => Vector3.Lerp(transform.position, transform.position + transform.rotation * _moveVector, _lerpValue);

    private Vector3 _currentVelocity;
    private Rigidbody _rb;
    private void OnValidate()
    {
        if (TryGetComponent<Rigidbody>(out _rb))
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        transform.localScale = Vector3.one;
    }

    private void Awake()
    {
        OldParents = new();
        _worldStartPos = transform.position;
        _startRot = new Quaternion()
        {
            w = transform.rotation.w,
            x = transform.rotation.x,
            y = transform.rotation.y,
            z = transform.rotation.z,
            eulerAngles = transform.rotation.eulerAngles
        };
        transform.position = GetRunTimePosition;
        if (TryGetComponent<Rigidbody>(out _rb))
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    //private void Update()
    //{
    //    _lerpValue.x = Mathf.Sin(Time.time) * 0.5f + 0.5f;
    //    _lerpValue.y = Mathf.Cos(Time.time) * 0.5f + 0.5f;
    //}

    private void FixedUpdate()
    {
        transform.position =
            Vector3.SmoothDamp(
                transform.position,
                GetRunTimePosition,
                ref _currentVelocity, 
                0.6f
            );

    }


    private Dictionary<Transform, Transform> OldParents;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<WalkOnPlatform>(out var plat))
        {
            OldParents.Add(other.transform, other.transform.parent);
            other.transform.parent = transform;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<WalkOnPlatform>(out var plat))
        {
            Vector3 temp = other.transform.position; // world pos
            other.transform.parent = OldParents[other.transform];
            other.transform.position = temp; // restore world position
            OldParents.Remove(other.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_worldStartPos , _worldStartPos + _startRot *_moveVector);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetRunTimePosition, transform.localScale);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.rotation * _moveVector);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(GetInspectorTimePosition, transform.localScale);
        }
    }
}
