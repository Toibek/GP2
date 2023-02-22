using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovingPlatformPush : MonoBehaviour
{
    [Header("Requirments")]
    [SerializeField]
    [Tooltip("Effected Platform")]
    private FreeMovingPlatform _platform;
    [SerializeField]
    private bool _moveForward = true;

    [Header("Config")]
    public UnityEngine.Events.UnityEvent OnMontisEnter;
    public UnityEngine.Events.UnityEvent OnMontisStay;
    public UnityEngine.Events.UnityEvent OnMontisExit;

    public bool _useEnter = true;
    public bool _useStay = false;
    public bool _useExit = true;


    private Movement _montisRefrence;
    private float _timePressed;

    private void Awake()
    {
        if (_platform == null) return;

        if (_moveForward)
        {
            OnMontisEnter.AddListener(() => _platform.StartMoveForward());
            OnMontisEnter.AddListener(() => Debug.Log("Start"));
            OnMontisExit.AddListener(() => _platform.StopMoveForward());
            OnMontisExit.AddListener(() => Debug.Log("exit"));
        }
        else
        {
            OnMontisEnter.AddListener(() => _platform.StartMoveBackward());
            OnMontisExit.AddListener(() => _platform.StopMoveBackward());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_useEnter) return;
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;

        if (other.TryGetComponent(out Movement mon))
        {
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
        if (!_useStay) return;
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;
        if (other.TryGetComponent(out Movement mon))
        {
            if (mon == _montisRefrence)
            {
                _timePressed += Time.deltaTime;
                OnMontisStay.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_useExit) return;
        if (other.gameObject.layer == LayerMask.GetMask("Trigger")) return;
        if (other.TryGetComponent(out Movement mon))
        {
            _montisRefrence = null;
            OnMontisExit.Invoke();
        }
    }

}
