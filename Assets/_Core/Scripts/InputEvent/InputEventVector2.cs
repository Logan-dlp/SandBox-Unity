using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new_" + nameof(InputEventVector2), menuName = "Input Events/Vector2")]
public class InputEventVector2 : ScriptableObject
{
    public Action<Vector2> actionVector2;

    public void InvokeEvent(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            actionVector2?.Invoke(ctx.ReadValue<Vector2>());
        }
        else if (ctx.canceled)
        {
            actionVector2?.Invoke(Vector2.zero);
        }
    }
}
