using System;
using UnityEngine;

public class TpsCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _yawPivot;
    [SerializeField] private Transform _pitchPivot;
    
    [SerializeField] private Vector2 _cameraClamp;
    
    [SerializeField] private float _sensitivity;

    private Vector2 _axisMovement = Vector2.zero;
    
    private float _rotationY;
    private float _rotationX;
    
    private bool _useYaw;
    
    private void LateUpdate()
    {
        _rotationY -= _axisMovement.x * _sensitivity;
        _rotationX += _axisMovement.y * _sensitivity;

        _rotationX = Mathf.Clamp(_rotationX, _cameraClamp.x, _cameraClamp.y);
        
        _pitchPivot.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        if (_useYaw)
        {
            _yawPivot.localRotation = Quaternion.Euler(0, _rotationY, 0);
        }
        else
        {
            _target.localRotation = Quaternion.Euler(0, _rotationY, 0);
        }
    }

    public void SetAxisMovement(Vector2 axis)
    {
        _axisMovement = axis;
    }

    [ContextMenu("Change Yaw")]
    private void ChangeYaw()
    {
        _yawPivot.localRotation = Quaternion.Euler(Vector3.zero);
        _useYaw = !_useYaw;
    }
}
