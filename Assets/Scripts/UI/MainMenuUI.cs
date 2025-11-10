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
        playButton.onClick.AddListener(HandlePlayButtonClick);
        quitButton.onClick.AddListener(HandleQuitButtonClick);
    }

    private void HandlePlayButtonClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void HandleQuitButtonClick()
    {
        Application.Quit();
    }
}
