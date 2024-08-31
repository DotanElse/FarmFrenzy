using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    
    void Start()
    {
        KitchenGameManager.Instance.OnGameStateChange += KitchenGameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    void Update()
    {
        countdownText.text = KitchenGameManager.Instance.getCountdownSeconds().ToString();
    }

    private void KitchenGameManager_OnStateChanged(object sender, KitchenGameManager.OnGameStateChangeArgs e)
    {
        if(e.state == KitchenGameManager.GameState.Countdown)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
