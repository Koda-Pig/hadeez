using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;

    private void Update()
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
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
