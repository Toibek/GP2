using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2PlatformRotation : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] [Tooltip("How many Eular angles it will turn per turning")]
    private float _degreesPerTurn;
    [SerializeField] [Tooltip("How long it takes to rotate and how much rotation to apply after so many seconds")]
    private AnimationCurve _RotationAfterSeconds = AnimationCurve.Linear(0,0,1,1);

    [Header("Requirements")]
    [SerializeField]
    private Vector2Platform _platform;
    [SerializeField]
    private Vector2PlatformForward _forwardSwitch;
    [SerializeField]
    private Vector2PlatformForward _backwardSwitch;

    private Movement _montisRefrence;

    private bool _isRotating = false;
    public bool Rotating { get => _isRotating; }

    public void Roatate()
    {
        if (_backwardSwitch != null && _forwardSwitch != null && !Rotating)
        {
            _forwardSwitch.RotateDegrees(_degreesPerTurn);
            _backwardSwitch.RotateDegrees(_degreesPerTurn);
            StartCoroutine("RotatePlatform");

        }

    }

    public IEnumerator RotatePlatform()
    {
        float _timer = 0f;
        _isRotating = true;
        Quaternion oldEularQuaternion = new Quaternion()
        {
            w = _platform.transform.rotation.w,
            x = _platform.transform.rotation.x,
            y = _platform.transform.rotation.y,
            z = _platform.transform.rotation.z,
            eulerAngles = _platform.transform.rotation.eulerAngles
        };
        Vector4 oldEularAngles = _platform.transform.rotation.eulerAngles;
        Vector4 newEularAngles = oldEularAngles + new Vector4(0, _degreesPerTurn, 0,0);
        yield return new WaitForFixedUpdate();
        while (_timer < _RotationAfterSeconds.keys[_RotationAfterSeconds.length - 1].time)
        {
            _platform.transform.rotation = Quaternion.Lerp(oldEularQuaternion, Quaternion.Euler(newEularAngles), _RotationAfterSeconds.Evaluate(_timer));
            _timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
            _platform.transform.rotation = Quaternion.Lerp(oldEularQuaternion, Quaternion.Euler(newEularAngles), _RotationAfterSeconds.Evaluate(_timer + 1));
        //_platform.transform.rotation = Quaternion.Euler(Vector4.Lerp(oldEularAngles, newEularAngles, _animationCurve.Evaluate(_timer + 1f)));

        _isRotating = false;
        yield return 0;
    }

}
