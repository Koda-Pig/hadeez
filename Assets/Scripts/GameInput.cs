using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = new(0, 0);

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            inputVector.y = +1;
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            inputVector.x = -1;
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            inputVector.y = -1;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
