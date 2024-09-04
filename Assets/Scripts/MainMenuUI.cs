using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    // Start is called before the first frame update
    void Awake()
    {
        quitButton.onClick.AddListener(() => {Application.Quit();});
        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        Time.timeScale =1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
