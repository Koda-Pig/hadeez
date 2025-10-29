using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
