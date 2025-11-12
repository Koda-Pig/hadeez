using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField]
    private Button closeButton;

    [SerializeField]
    private Button moveUpButton;

    [SerializeField]
    private Button moveDownButton;

    [SerializeField]
    private Button moveLeftButton;

    [SerializeField]
    private Button moveRightButton;

    [SerializeField]
    private Button interactButton;

    [SerializeField]
    private Button interactAltButton;

    [SerializeField]
    private Button pauseButton;

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
    private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        Instance = this;

        soundFXSlider.onValueChanged.AddListener((val) => SoundManager.Instance.SetVolume(val));
        musicSlider.onValueChanged.AddListener((val) => MusicManager.Instance.SetVolume(val));

        closeButton.onClick.AddListener(() => Hide());
        moveUpButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveUp));
        moveDownButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveDown));
        moveLeftButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveLeft));
        moveRightButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.MoveRight));
        interactButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Interact));
        interactAltButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.InteractAlternate));
        pauseButton.onClick.AddListener(() => RebindBinding(GameInput.Binding.Pause));
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGamePaused;
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
        Func<GameInput.Binding, string> getText = GameInput.Instance.GetBindingText;

        moveUpText.text = getText(GameInput.Binding.MoveUp);
        moveDownText.text = getText(GameInput.Binding.MoveDown);
        moveLeftText.text = getText(GameInput.Binding.MoveLeft);
        moveRightText.text = getText(GameInput.Binding.MoveRight);
        interactText.text = getText(GameInput.Binding.Interact);
        interactAltText.text = getText(GameInput.Binding.InteractAlternate);
        pauseText.text = getText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
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

    private void RebindBinding(GameInput.Binding binding)
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
