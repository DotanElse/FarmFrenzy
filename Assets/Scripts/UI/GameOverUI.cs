using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDelivered;
    
    void Start()
    {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnStateChanged;
        gameObject.SetActive(false);
        recipesDelivered.text = DeliveryManager.Instance.GetRecipeCompleted().ToString();
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
