using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreeMovingPlatform : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3 _velocity;

    [SerializeField]
    private Transform[] _overlapChecksRotation = new Transform[2];
    [SerializeField]
    private Vector3 _overlapRotation = new Vector3(1, 1, 5);

    [SerializeField]
    private Transform[] _overlapChecksMoving = new Transform[2];
    [SerializeField]
    private Vector3 _overlapMoving = new Vector3(1, 1, 5);

    [SerializeField]
    private float _forwardSpeed = 1f;
    private float _speedTimer;

    [SerializeField]
    private LayerMask _collisionLayerMask = 1<<8;

    [SerializeField]
    private AnimationCurve _acceleration = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField]
    private AnimationCurve _rotationCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Space]
    [Header("Moving Events")]
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnMoveStart;
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnMoveInterupted;
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnMoveEnd;

    [Header("Rotation Events")]
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnRotationStart;
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnRotationInterupted;
    [SerializeField]
    private UnityEngine.Events.UnityEvent OnRotationEnd;

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
        OldParents = new Dictionary<Transform, Transform>();
    }

    private void Update()
    {
        if (_isRotating)
        {
            if (!_isRotatingBack && CheckOverlapRotation())
            {
                _isRotatingBack = true;
                StopCoroutine("Rotating");
                OnRotationInterupted.Invoke();
                StartCoroutine("GoBack");
            }
            _velocity = Vector3.zero;
        }
        else
        {
            if (_isMovingForward && CheckOverlapMovingForward())
            {
                StopMoveForward();
            }
            else if (_isMovingBackward && CheckOverlapMovingBackward())
            {
                StopMoveBackward();
            }
            else if (!_isMovingForward && !_isMovingBackward)
            {
                //_velocity = Vector3.zero;
            }
        }
        transform.position += _velocity * Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (_isRotating) return;
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
        Debug.Log("startMoveF");
        _isMovingForward = true;
        _speedTimer = 0;

        if (_isMovingBackward)
            StopMoveBackward(true);
        _isMovingBackward = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;


        StartCoroutine("MoveForward");

        OnMoveStart?.Invoke();
    }

    public void StartMoveBackward()
    {
        if (_isRotating) return;
        Debug.Log("startMoveB");

        _isMovingBackward = true;
        _speedTimer = 0;

        if (_isMovingForward)
            StopMoveForward(true);
        _isMovingForward = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        StartCoroutine("MoveBackward");

        OnMoveStart?.Invoke();
    }

    public void StopMoveForward(bool interupted = false)
    {
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _isMovingForward = false;

        if (!interupted)
            OnMoveEnd?.Invoke();
        else if (interupted)
            OnMoveInterupted?.Invoke();
        _velocity = Vector3.zero;
        Debug.Log("test");
        StopCoroutine("MoveForward");
    }

    public void StopMoveBackward(bool interupted = false)
    {
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        _isMovingBackward = false;

        if (!interupted)
            OnMoveEnd?.Invoke();
        else if (interupted)
            OnMoveInterupted?.Invoke();
        _velocity = Vector3.zero;
        StopCoroutine("MoveBackward");
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            if (_speedTimer < _acceleration[_acceleration.length - 1].time)
            {
                _speedTimer += Time.deltaTime;
            }
            _velocity = _acceleration.Evaluate(_speedTimer) * transform.forward * _forwardSpeed;

            Debug.Log("f");

            yield return new WaitForEndOfFrame();
        }
        yield return 0;
    }

    private IEnumerator MoveBackward()
    {
        while (true)
        {
            if (_speedTimer < _acceleration[_acceleration.length - 1].time)
                _speedTimer += Time.deltaTime;
            _velocity = _acceleration.Evaluate(_speedTimer) * -transform.forward * _forwardSpeed;
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
        if (_isRotating) return;

        StopMoveForward();
        StopMoveBackward();
        StartCoroutine("Rotating", rotation);
        OnRotationStart.Invoke();
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
            transform.rotation =(Quaternion.Lerp(lastRotation, newRotation, _rotationCurve.Evaluate(_timer)));
            
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = (Quaternion.Lerp(lastRotation, newRotation, _rotationCurve.Evaluate(1f)));

        _isRotating = false;
        OnRotationEnd.Invoke();
        yield return 0;
    }

    private bool CheckOverlapRotation()
    {
        Vector3 pos = Vector3.zero;
        Collider[] hits = new Collider[2];
        for (int i = 0; i < _overlapChecksRotation.Length; i++)
        {
            pos = _overlapChecksRotation[i].transform.position;
            if (Physics.OverlapBoxNonAlloc(pos, _overlapRotation, hits ,transform.rotation, _collisionLayerMask, QueryTriggerInteraction.Collide) != 0)
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
        if (Physics.OverlapBoxNonAlloc(pos, _overlapMoving, hits, transform.rotation, _collisionLayerMask, QueryTriggerInteraction.Collide) != 0)
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
        if (Physics.OverlapBoxNonAlloc(pos, _overlapMoving, hits, transform.rotation, _collisionLayerMask, QueryTriggerInteraction.Collide) != 0)
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
            transform.rotation = (Quaternion.Lerp(startRot, lastRotation, _rotationCurve.Evaluate(_timer)));
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = (Quaternion.Lerp(startRot, lastRotation, _rotationCurve.Evaluate(_timer)));

        _isRotating = false;
        _isRotatingBack = false;
        OnRotationEnd.Invoke();
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
