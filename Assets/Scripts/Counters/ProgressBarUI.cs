using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGO;

    private IHasProgress hasProgress;
    // Start is called before the first frame update
    void Start()
    {
        hasProgress = hasProgressGO.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError($"component doesnt have progress: {hasProgressGO}");

        }
        hasProgress.OnProgressChange += setBarProgress;
        barImage.fillAmount = 0f;
        Hide();
    }

    void setBarProgress(object sender, IHasProgress.OnProgressChangeArgs e)
    {
        if(e.normalizedProgress < 1f && e.normalizedProgress > 0f)
        {
            barImage.fillAmount = e.normalizedProgress;
            Debug.Log($"progressed, {e.normalizedProgress}");
            Show();
        }
        else
        {
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
