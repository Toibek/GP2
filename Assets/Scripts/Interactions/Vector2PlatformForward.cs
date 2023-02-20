using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2PlatformForward : MonoBehaviour
{
    private Quaternion _rot;
    private Vector3 _rotEular;
    private float _timePressed;
    [Header("Config")]
    [SerializeField] [Tooltip("Direction that will count as forward on start")]
    private Vector2 _forwardDir = Vector2.up;

    [SerializeField] [Tooltip("How fast platform will go after x seconds last value represent max velocity in procent/second")]
    private AnimationCurve _accelerationOverTime = AnimationCurve.Linear(0,0,1,1);

    public UnityEngine.Events.UnityEvent OnMontisEnter;
    public UnityEngine.Events.UnityEvent OnMontisStay;
    public UnityEngine.Events.UnityEvent OnMontisExit;

    [Header("Requirments")]
    [SerializeField] [Tooltip("Affected Platform")]
    private Vector2Platform _platform;
    [SerializeField]
    private Vector2PlatformRotation _RotatingPlatformScript;

    private Movement _montisRefrence;

    public Vector3 RotEular { get => _rotEular; set { _rotEular = value; UpdateQuaternion(); } }
    public Vector3 MovingDirection { get => _forwardDir; set { _forwardDir = value; } }
    
    [ContextMenu("TestRotation")]
    public void TestingAddRotation()
    {
        RotEular = _rot.eulerAngles + new Vector3(0, 0, 45);
    }

    public void RotateDegrees(float degrees)
    {
        RotEular = _rot.eulerAngles + new Vector3(0, 0, degrees);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;

        if (other.TryGetComponent(out Movement mon)){
            if (other.GetComponentInChildren<Montis>() != null)
            {
                _montisRefrence = mon;
                OnMontisEnter.Invoke();
                _timePressed = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;
        if (other.TryGetComponent(out Movement mon))
        {
            if (mon == _montisRefrence)
            {
                _timePressed += Time.deltaTime;
                GoForward();
                OnMontisStay.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;
        if (other.TryGetComponent(out Movement mon))
        {
            _montisRefrence = null;
            OnMontisExit.Invoke();
        }
    }

    public void GoForward()
    {
        if (_platform == null || _RotatingPlatformScript == null || _RotatingPlatformScript.Rotating) return;

        _platform.AddProcent(_rot * (_forwardDir * _accelerationOverTime.Evaluate(_timePressed) * Time.deltaTime));
        Debug.Log(_rot * (_forwardDir * _accelerationOverTime.Evaluate(_timePressed)));
    }

    private void UpdateQuaternion()
    {
        _rot = Quaternion.Euler(_rotEular);
        _rotEular = _rot.eulerAngles;
    }
}
