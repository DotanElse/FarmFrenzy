using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDelivered;
    [SerializeField] private Button returnButton;
    
    void Start()
    {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnStateChanged;
        gameObject.SetActive(false);
        recipesDelivered.text = DeliveryManager.Instance.GetRecipeCompleted().ToString();
        returnButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void KitchenGameManager_OnStateChanged(object sender, KitchenGameManager.OnGameStateChangeArgs e)
    {
        if(e.state == KitchenGameManager.GameState.GameOver)
        {
            recipesDelivered.text = DeliveryManager.Instance.GetRecipeCompleted().ToString();
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
