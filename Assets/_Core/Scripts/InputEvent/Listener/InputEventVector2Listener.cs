using System;
using UnityEngine;
using UnityEngine.Events;

public class InputEventVector2Listener : MonoBehaviour
{
    [SerializeField] private InputEventVector2 _inputEventVector2;
    [SerializeField] private UnityEvent<Vector2> _callbacks;

    private void OnEnable()
    {
        _inputEventVector2.actionVector2 += InvokeEvent;
    }

    private void OnDisable()
    {
        _inputEventVector2.actionVector2 -= InvokeEvent;
    }

    private void InvokeEvent(Vector2 vector2)
    {
        _callbacks?.Invoke(vector2);
    }
}
