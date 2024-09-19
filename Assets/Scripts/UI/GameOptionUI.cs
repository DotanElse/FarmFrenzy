using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionUI : MonoBehaviour
{
    [SerializeField] Button sfxUpButton;
    [SerializeField] Button sfxDownButton;
    [SerializeField] Button SoundUpButton;
    [SerializeField] Button SoundDownButton;
    [SerializeField] Button returnButton;
    [SerializeField] Button mainMenuButton;

    [SerializeField] private TextMeshProUGUI sfxValue;
    [SerializeField] private TextMeshProUGUI soundValue;


    // Start is called before the first frame update
    void Start()
    {
        UpdateSFXUI(SoundManager.Instance.GetSFXVolume());
        UpdateSoundUI(SoundManager.Instance.GetSoundVolume());
        sfxUpButton.onClick.AddListener(() => {
            UpdateSFXUI(SoundManager.Instance.ChangeVolume(true));
        });
        sfxDownButton.onClick.AddListener(() => {
            UpdateSFXUI(SoundManager.Instance.ChangeVolume(false));
        });
        SoundUpButton.onClick.AddListener(() => {
            UpdateSoundUI(SoundManager.Instance.ChangeBGMVolume(true));
        });
        SoundDownButton.onClick.AddListener(() => {
            UpdateSoundUI(SoundManager.Instance.ChangeBGMVolume(false));
        });
    

        returnButton.onClick.AddListener(()=> 
        {
            Hide();
            KitchenGameManager.Instance.ToggleGamePause();
        });
        mainMenuButton.onClick.AddListener(()=> 
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });


        GamePauseUI.Instance.OnOptionClick += OnGameOptionsShow;
        KitchenGameManager.Instance.OnGameUnpaused += OnGameOptionsHide;
        Hide();
    }

    private void UpdateSFXUI(int value)
    {
        Debug.Log($"got {value} to update");
        sfxValue.text = value.ToString();
    }
    private void UpdateSoundUI(int value)
    {
        Debug.Log($"got {value} to update");
        soundValue.text = value.ToString();
    }


    private void OnGameOptionsHide(object sender, EventArgs e)
    {
        Hide();
    }

    private void OnGameOptionsShow(object sender, EventArgs e)
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
