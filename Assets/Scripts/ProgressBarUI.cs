using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter; 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("init");
        cuttingCounter.OnProgressChange += setBarProgress;
        barImage.fillAmount = 0f;
        Hide();
    }

    void setBarProgress(object sender, CuttingCounter.OnProgressChangeArgs e)
    {
        if(e.normalizedProgress != 1f)
        {
            barImage.fillAmount = e.normalizedProgress;
            Debug.Log($"progressed, {e.normalizedProgress}");
            Show();
        }
        else{
            Hide();
        }
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
