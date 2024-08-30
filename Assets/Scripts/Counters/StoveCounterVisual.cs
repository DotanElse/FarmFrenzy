using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOn;
    [SerializeField] GameObject stoveParticles;
    // Start is called before the first frame update
    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;

    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeArgs e)
    {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried)
        {
            stoveOn.SetActive(true);
            stoveParticles.SetActive(true);
        }
        else
        {
            stoveOn.SetActive(false);
            stoveParticles.SetActive(false);
        }
    }
}
