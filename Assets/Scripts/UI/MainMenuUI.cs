using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
        quitButton.onClick.AddListener(() => Application.Quit());

        // set game play speed back to 1 (0 is game paused)
        Time.timeScale = 1f;
    }
}
