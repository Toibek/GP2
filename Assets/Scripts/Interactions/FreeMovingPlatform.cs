using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreeMovingPlatform : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField]
    private Transform[] _overlapChecksRotation = new Transform[2];
    [SerializeField]
    private Vector3 _overlapRotation = new Vector3(1,1,5);

    [SerializeField]
    private Transform[] _overlapChecksMoving = new Transform[2];
    [SerializeField]
    private Vector3 _overlapMoving = new Vector3(1, 1, 5);

    [SerializeField]
    private float _forwardSpeed = 1f;
    private float _speedTimer;


    [SerializeField]
    private AnimationCurve _acceleration = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField]
    private AnimationCurve _rotationCurve = AnimationCurve.Linear(0,0,1,1);

    private bool _isRotating;
    private bool _isRotatingBack;
    private bool _isMovingForward;
    private bool _isMovingBackward;
    private Quaternion lastRotation;

    public bool ShowGizmo = false;

    private Dictionary<Transform, Transform> OldParents;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isRotating)
        {
            if (!_isRotatingBack && CheckOverlapRotation())
            {
                _isRotatingBack = true;
                StopCoroutine("Rotating");
                AudioManager.S_PlayOneShotSound(Sound.Names.UI_SelectNegative);
                StartCoroutine("GoBack");
            }
            _rb.velocity = Vector3.zero;
        }
        else
        {
            if (_isMovingForward && CheckOverlapMovingForward())
            {
                Debug.Log("Moving Overlap forward");
            }
            if (_isMovingBackward && CheckOverlapMovingBackward())
            {
                Debug.Log("Moving Overlap backward");
            }
        }
    }

    [ContextMenu("Move Forward")]
    public void MoveForwardTesting()
    {
        StartMoveForward();
    }

    [ContextMenu("Move backward")]
    public void MoveBackwardTesting()
    {
        StartMoveBackward();
    }
    [ContextMenu("Rotation testing")]
    public void RotateTest()
    {
        Rotate(90f);
    }

    public void StartMoveForward()
    {
        if (_isRotating) return;
        _isMovingForward = true;
        _speedTimer = 0;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        StopCoroutine("MoveBackward");
        _isMovingBackward = false;
        StartCoroutine("MoveForward");
    }

    public void StartMoveBackward()
    {
        if (_isRotating) return;
        _speedTimer = 0;
        _isMovingBackward = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        StopCoroutine("MoveForward");
        _isMovingForward = false;
        StartCoroutine("MoveBackward");
    }

    public void StopMoveForward()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _isMovingForward = false;
        StopCoroutine("MoveForward");
    }

    public void StopMoveBackward()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _isMovingBackward = false;
        StopCoroutine("MoveBackward");
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            if (_speedTimer < _acceleration[_acceleration.length - 1].time)
                _speedTimer += Time.deltaTime;
            _rb.velocity = _acceleration.Evaluate(_speedTimer) * transform.forward * _forwardSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoveBackward()
    {
        while (true)
        {
            if (_speedTimer < _acceleration[_acceleration.length - 1].time)
                _speedTimer += Time.deltaTime;
            _rb.velocity = _acceleration.Evaluate(_speedTimer) * -transform.forward * _forwardSpeed;
            yield return new WaitForEndOfFrame();
        }
    }

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

    internal void Rotate(float rotation)
    {
        if (!_isRotating)
            StartCoroutine("Rotating", rotation);
    }

    internal IEnumerator Rotating(float rotation)
    {
        lastRotation = transform.rotation;
        Quaternion newRotation = lastRotation * Quaternion.Euler(0, rotation, 0);
        _isRotating = true;
        float _timer = 0f;

        while (_timer < 1)
        {
            _timer += Time.fixedDeltaTime;
            _rb.MoveRotation(Quaternion.Lerp(lastRotation, newRotation, _rotationCurve.Evaluate(_timer)));
            
            yield return new WaitForFixedUpdate();
        }
        _rb.MoveRotation(Quaternion.Lerp(lastRotation, newRotation, _rotationCurve.Evaluate(1f)));

        _isRotating = false;
        yield return 0;
    }

    private bool CheckOverlapRotation()
    {
        Vector3 pos = Vector3.zero;
        Collider[] hits = new Collider[2];
        for (int i = 0; i < _overlapChecksRotation.Length; i++)
        {
            pos = _overlapChecksRotation[i].transform.position;
            if (Physics.OverlapBoxNonAlloc(pos, _overlapRotation, hits ,transform.rotation, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) != 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckOverlapMovingForward()
    {
        Vector3 pos = Vector3.zero;
        Collider[] hits = new Collider[2];
        pos = _overlapChecksMoving[0].transform.position;
        if (Physics.OverlapBoxNonAlloc(pos, _overlapMoving, hits, transform.rotation, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) != 0)
        {
            return true;
        }
        return false;
    }

    private bool CheckOverlapMovingBackward()
    {
        Vector3 pos = Vector3.zero;
        Collider[] hits = new Collider[2];
        pos = _overlapChecksMoving[1].transform.position;
        if (Physics.OverlapBoxNonAlloc(pos, _overlapMoving, hits, transform.rotation, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) != 0)
        {
            return true;
        }
        return false;
    }

    internal IEnumerator GoBack()
    {
        Quaternion startRot = transform.rotation;
        float _timer = 0f;

        while (_timer < 1)
        {
            _timer += Time.fixedDeltaTime;
            _rb.MoveRotation(Quaternion.Lerp(startRot, lastRotation, _rotationCurve.Evaluate(_timer)));
            yield return new WaitForFixedUpdate();
        }

        _isRotating = false;
        _isRotatingBack = false;
        yield return 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isRotating || _isRotatingBack) return;
        if (collision.gameObject.layer == LayerMask.GetMask("Ground"))
        {
            _isRotatingBack = true;
            StopCoroutine("Rotating");
            AudioManager.S_PlayOneShotSound(Sound.Names.UI_SelectNegative);
            StartCoroutine("GoBack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!ShowGizmo) return;
        Vector3 pos = Vector3.zero;
        Collider[] hits = new Collider[2];
        Gizmos.matrix = transform.localToWorldMatrix;
        for (int i = 0; i < _overlapChecksRotation.Length; i++)
        {
            if (_overlapChecksRotation[i] == null) continue;

            pos = _overlapChecksRotation[i].transform.localPosition;

            Gizmos.DrawCube(pos, _overlapRotation);
            //Physics.OverlapBoxNonAlloc(pos, new Vector3(0, 1, 5), hits, transform.rotation, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) != 0
            
        }

        for (int i = 0; i < _overlapChecksMoving.Length; i++)
        {
            if (_overlapChecksMoving[i] == null) continue;

            pos = _overlapChecksMoving[i].transform.localPosition;

            Gizmos.DrawCube(pos, _overlapMoving);
            //Physics.OverlapBoxNonAlloc(pos, new Vector3(0, 1, 5), hits, transform.rotation, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore) != 0

        }
    }
}
