using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private ClearCounter clearCounter;

    [SerializeField]
    private GameObject visualGameObject;

    // We need Player object, which inits on `Awake`.
    // To ensure this script runs after that one
    // we init this class on 'Start' which comes after.
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(
        object sender,
        Player.OnSelectedCounterChangedEventArgs e
    )
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
