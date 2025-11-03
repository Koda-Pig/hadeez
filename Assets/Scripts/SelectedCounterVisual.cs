using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField]
    private BaseCounter baseCounter;

    [SerializeField]
    private GameObject[] visualGameObjects;

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
        if (e.selectedCounter == baseCounter)
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
        foreach (GameObject gameObject in visualGameObjects)
        {
            gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject gameObject in visualGameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
