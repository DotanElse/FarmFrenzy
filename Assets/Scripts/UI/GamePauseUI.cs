using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(() => {Debug.Log("Clicked"); KitchenGameManager.Instance.ToggleGamePause();});
        quitButton.onClick.AddListener(() => {Loader.Load(Loader.Scene.MainMenuScene);});
        KitchenGameManager.Instance.OnGamePaused += OnGamePausedUI;
        KitchenGameManager.Instance.OnGameUnpaused += OnGameUnpausedUI;
        Hide();
    }

    private void OnGameUnpausedUI(object sender, EventArgs e)
    {
        Hide();
    }

    private void OnGamePausedUI(object sender, EventArgs e)
    {
        Show();
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
