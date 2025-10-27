using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("pressing W");
        }
        else
        {
            Debug.Log("not pressing anything");
        }
    }
}
