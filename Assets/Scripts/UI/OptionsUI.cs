using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Binding = GameInput.Binding;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField]
    private Button closeBtn;

    [SerializeField]
    private Button moveUpBtn;

    [SerializeField]
    private Button moveDownBtn;

    [SerializeField]
    private Button moveLeftBtn;

    [SerializeField]
    private Button moveRightBtn;

    [SerializeField]
    private Button interactBtn;

    [SerializeField]
    private Button interactAltBtn;

    [SerializeField]
    private Button pauseBtn;

    [SerializeField]
    private Button gamepadInteractBtn;

    [SerializeField]
    private Button gamepadInteractAltBtn;

    [SerializeField]
    private Button gamepadPauseBtn;

    [SerializeField]
    private Slider soundFXSlider;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private TextMeshProUGUI moveUpText;

    [SerializeField]
    private TextMeshProUGUI moveDownText;

    [SerializeField]
    private TextMeshProUGUI moveLeftText;

    [SerializeField]
    private TextMeshProUGUI moveRightText;

    [SerializeField]
    private TextMeshProUGUI interactText;

    [SerializeField]
    private TextMeshProUGUI interactAltText;

    [SerializeField]
    private TextMeshProUGUI pauseText;

    [SerializeField]
    private TextMeshProUGUI gamepadInteractText;

    [SerializeField]
    private TextMeshProUGUI gamepadInteractAltText;

    [SerializeField]
    private TextMeshProUGUI gamepadPauseText;

    [SerializeField]
    private Transform pressToRebindKeyTransform;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        soundFXSlider.onValueChanged.AddListener(
            (val) => SoundManager.Instance.SetVolume(val)
        );
        musicSlider.onValueChanged.AddListener(
            (val) => MusicManager.Instance.SetVolume(val)
        );

        closeBtn.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
        moveUpBtn.onClick.AddListener(() => RebindBinding(Binding.MoveUp));
        moveDownBtn.onClick.AddListener(() => RebindBinding(Binding.MoveDown));
        moveLeftBtn.onClick.AddListener(() => RebindBinding(Binding.MoveLeft));
        moveRightBtn.onClick.AddListener(() =>
            RebindBinding(Binding.MoveRight)
        );
        interactBtn.onClick.AddListener(() => RebindBinding(Binding.Interact));
        interactAltBtn.onClick.AddListener(() =>
            RebindBinding(Binding.InteractAlternate)
        );
        pauseBtn.onClick.AddListener(() => RebindBinding(Binding.Pause));
        gamepadInteractBtn.onClick.AddListener(() =>
            RebindBinding(Binding.GamepadInteract)
        );
        gamepadInteractAltBtn.onClick.AddListener(() =>
            RebindBinding(Binding.GamepadInteractAlternate)
        );
        gamepadPauseBtn.onClick.AddListener(() =>
            RebindBinding(Binding.GamepadPause)
        );
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused +=
            KitchenGameManager_OnGamePaused;
        soundFXSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGamePaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        Func<Binding, string> getText = GameInput.Instance.GetBindingText;

        moveUpText.text = getText(Binding.MoveUp);
        moveDownText.text = getText(Binding.MoveDown);
        moveLeftText.text = getText(Binding.MoveLeft);
        moveRightText.text = getText(Binding.MoveRight);
        interactText.text = getText(Binding.Interact);
        interactAltText.text = getText(Binding.InteractAlternate);
        pauseText.text = getText(Binding.Pause);
        gamepadInteractText.text = getText(Binding.GamepadInteract);
        gamepadInteractAltText.text = getText(Binding.GamepadInteractAlternate);
        gamepadPauseText.text = getText(Binding.GamepadPause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        closeBtn.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(
            binding,
            () =>
            {
                HidePressToRebindKey();
                UpdateVisual();
            }
        );
    }
}
