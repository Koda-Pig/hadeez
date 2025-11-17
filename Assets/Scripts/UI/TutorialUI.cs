using System;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI keyMoveUpText;

    [SerializeField]
    private TextMeshProUGUI keyMoveDownText;

    [SerializeField]
    private TextMeshProUGUI keyMoveLeftText;

    [SerializeField]
    private TextMeshProUGUI keyMoveRightText;

    [SerializeField]
    private TextMeshProUGUI keyInteractText;

    [SerializeField]
    private TextMeshProUGUI keyInteractAltText;

    [SerializeField]
    private TextMeshProUGUI keyPauseText;

    [SerializeField]
    private TextMeshProUGUI keyGamepadInteractText;

    [SerializeField]
    private TextMeshProUGUI keyGamepadInteractAltText;

    [SerializeField]
    private TextMeshProUGUI keyGamepadPauseText;

    private void Start()
    {
        UpdateVisual();
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitchenGameManager.Instance.OnStateChanged +=
            KitchenGameManager_OnStateChanged;
        Show();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void GameInput_OnBindingRebind(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Func<GameInput.Binding, string> getText = GameInput
            .Instance
            .GetBindingText;

        keyMoveUpText.text = getText(GameInput.Binding.MoveUp);
        keyMoveDownText.text = getText(GameInput.Binding.MoveDown);
        keyMoveLeftText.text = getText(GameInput.Binding.MoveLeft);
        keyMoveRightText.text = getText(GameInput.Binding.MoveRight);
        keyInteractText.text = getText(GameInput.Binding.Interact);
        keyInteractAltText.text = getText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = getText(GameInput.Binding.Pause);
        keyGamepadInteractText.text = getText(
            GameInput.Binding.GamepadInteract
        );
        keyGamepadInteractAltText.text = getText(
            GameInput.Binding.GamepadInteractAlternate
        );
        keyGamepadPauseText.text = getText(GameInput.Binding.GamepadPause);
    }
}
