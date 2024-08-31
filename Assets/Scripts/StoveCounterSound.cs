using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource sound;
    // Start is called before the first frame update
    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangeArgs e)
    {
        if(e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying)
        {
            sound.Play();
        }
        else
        {
            sound.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
