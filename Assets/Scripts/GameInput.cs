using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    private InputSystem_Actions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    public static GameInput Instance { get; private set; }

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        GamepadInteract,
        GamepadInteractAlternate,
        GamepadPause,
    }

    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputSystem_Actions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        // assigning a listener to an event
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        // clean up event listeners
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        InputBinding bindingToDisplay;

        switch (binding)
        {
            default:
            // the directional movement buttons are a composite binding. Hence the difference with
            // the array index for these buttons in comparison to the simple single binding keys
            case Binding.MoveUp:
                bindingToDisplay = playerInputActions.Player.Move.bindings[1];
                break;
            case Binding.MoveDown:
                bindingToDisplay = playerInputActions.Player.Move.bindings[2];
                break;
            case Binding.MoveLeft:
                bindingToDisplay = playerInputActions.Player.Move.bindings[3];
                break;
            case Binding.MoveRight:
                bindingToDisplay = playerInputActions.Player.Move.bindings[4];
                break;
            case Binding.Interact:
                bindingToDisplay = playerInputActions.Player.Interact.bindings[0];
                break;
            case Binding.InteractAlternate:
                bindingToDisplay = playerInputActions.Player.InteractAlternate.bindings[0];
                break;
            case Binding.Pause:
                bindingToDisplay = playerInputActions.Player.Pause.bindings[0];
                break;
            case Binding.GamepadInteract:
                bindingToDisplay = playerInputActions.Player.Interact.bindings[1];
                break;
            case Binding.GamepadInteractAlternate:
                bindingToDisplay = playerInputActions.Player.InteractAlternate.bindings[1];
                break;
            case Binding.GamepadPause:
                bindingToDisplay = playerInputActions.Player.Pause.bindings[1];
                break;
        }

        if (bindingToDisplay == null)
        {
            return "";
        }

        string displayString = bindingToDisplay.ToDisplayString();
        return displayString.Length > 3 ? displayString[..3] : displayString;
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.MoveUp:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamepadInteract:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteractAlternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.GamepadPause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
        }

        inputAction
            .PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Player.Enable();
                onActionRebound();
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
