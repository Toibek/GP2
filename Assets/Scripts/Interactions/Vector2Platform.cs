using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class Vector2Platform : MonoBehaviour
{
    [Header("Note: \n" +
        " GreenBox = +Y | RedBox = +X\n" +
        "*Set Colliders on children\n" +
        "*Objets that should move with this platform \n should have the 'WalkOnPlatform' script \n\n" +
        "*Rotation Works just not with Gizmos.\n" +
        "*DON'T CHANGE SCALE ON THIS GAMEOBJECT!!!")]
    [Space]
    [Tooltip("How big the area the platform can move in")]
    [SerializeField]
    private Vector2 _restraints = Vector2.one* 2;


    [Tooltip("Where on the area it will start")]
    [SerializeField]
    private Vector2 _startPos = Vector2.one;
    private Vector3 _worldStartPos;
    private Quaternion _startRot;
    [Header("GameTime Value")]
    [SerializeField]
    private Vector2 _lerpValue;

    private Vector3 _currentVelocity;
    private Rigidbody _rb;


    internal void AddProcent(Vector2 delta)
    {
        if (_lerpValue.x == 1 || _lerpValue.x == 0)
            delta.y = 0;
        if (_lerpValue.y == 1 || _lerpValue.y == 0)
            delta.x = 0;
        _lerpValue = new Vector2(
            Mathf.Clamp(_lerpValue.x + delta.x, 0, 1),
            Mathf.Clamp(_lerpValue.y + delta.y, 0, 1)
            );

    }

    private void OnValidate()
    {
        _startPos = new Vector2(
            Mathf.Clamp(_startPos.x, 0, _restraints.x),
            Mathf.Clamp(_startPos.y, 0, _restraints.y)
            );
        _lerpValue = new Vector2(
            Mathf.Clamp(_lerpValue.x, 0, 1),
            Mathf.Clamp(_lerpValue.y, 0, 1)
            );
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
        _startRot = new Quaternion() {
            w = transform.rotation.w,
            x = transform.rotation.x,
            y = transform.rotation.y,
            z = transform.rotation.z,
            eulerAngles = transform.rotation.eulerAngles
        };

        _lerpValue = new Vector2(
            _startPos.x / _restraints.x,
            _startPos.y / _restraints.y
            );
        transform.position = _startRot * transform.position + new Vector3(_startPos.x, 0, _startPos.y);
        if (TryGetComponent<Rigidbody>(out _rb))
        {
            _rb.useGravity = false;
            _rb.isKinematic = true;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void FixedUpdate()
    {

        transform.position = 
            Vector3.SmoothDamp(
                transform.position,
                _worldStartPos + _startRot * (new Vector3(
                    Mathf.Lerp(_worldStartPos.x, _worldStartPos.x + _restraints.x, _lerpValue.x),   // x
                    _worldStartPos.y,                                                               // y
                    Mathf.Lerp(_worldStartPos.z, _worldStartPos.z + _restraints.y, _lerpValue.y)    // z
                ) - _worldStartPos),
                ref _currentVelocity, 0.6f
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
            Gizmos.DrawWireCube(_worldStartPos + _startRot * (new Vector3(_restraints.x, 0, _restraints.y) * 0.5f), new Vector3(_restraints.x, 0, _restraints.y));
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_worldStartPos + _startRot * (new Vector3(_restraints.x * 0.5f, 0, _restraints.y)), Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_worldStartPos + _startRot * (new Vector3(_restraints.x, 0, _restraints.y * 0.5f)), Vector3.one);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube( transform.position + transform.rotation * (new Vector3(_restraints.x, 0, _restraints.y) * 0.5f), new Vector3(_restraints.x, 0, _restraints.y));
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube( transform.position + transform.rotation * new Vector3(_startPos.x, 0, _startPos.y), transform.localScale);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + _startRot * (new Vector3(_restraints.x * 0.5f, 0, _restraints.y)), Vector3.one);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + _startRot * (new Vector3(_restraints.x, 0, _restraints.y * 0.5f)), Vector3.one);
        }
    }
}
