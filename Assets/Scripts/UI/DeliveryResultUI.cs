using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private TextMeshProUGUI messageText;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Color failedColor;

    [SerializeField]
    private Sprite successSprite;

    [SerializeField]
    private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        UpdateVisual(wasSuccess: true);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        UpdateVisual(wasSuccess: false);
    }

    private void UpdateVisual(bool wasSuccess)
    {
        backgroundImage.color = wasSuccess ? successColor : failedColor;
        iconImage.sprite = wasSuccess ? successSprite : failedSprite;
        messageText.text = wasSuccess ? "DELIVERY\nSUCCESS" : "DELIVERY\nFAILED";
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
    }
}
