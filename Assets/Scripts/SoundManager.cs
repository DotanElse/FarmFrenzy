using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private const string SOUND_EFFECTS_VOLUME = "SFX_Volume";
    private const string MUSIC_VOLUME = "MUSIC_Volume";
    [SerializeField] AudioClipsRefsSO audioClipsRefsSO; 
    [SerializeField] AudioSource music;
    public static SoundManager Instance {get; private set;}
    int volume;
    int bgmVolume;

    void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetInt(SOUND_EFFECTS_VOLUME, 5);
        bgmVolume = PlayerPrefs.GetInt(MUSIC_VOLUME, 5);
    }
    public void Start()
    {
        CuttingCounter.OnAnyCut += Chop;
        DeliveryManager.OnFailDelivery += FailDelivery;
        DeliveryManager.OnSuccessDelivery += SuccessDelivery;
        Player.Instance.OnPickedSomething += OnObjectPickup;
        BaseCounter.OnObjectDrop += OnObjectDrop;
        TrashCounter.OnObjectTrashed += OnObjectTrashed;
        Player.Instance.OnMovement += OnPlayerMovement;
    }

    public int GetSoundVolume()
    {
        return PlayerPrefs.GetInt(MUSIC_VOLUME, 5);
    }

    public int GetSFXVolume()
    {
        return PlayerPrefs.GetInt(SOUND_EFFECTS_VOLUME, 5);
    }

    public int ChangeVolume(bool increase)
    {
        Debug.Log($"Before changing {volume}");
        if(increase && volume < 10)
        {
            volume++;
            Debug.Log("Increased");
        }
        if(!increase && volume > 0)
        {
            volume--;
            Debug.Log("Decreased");
        }
            
        Debug.Log($"After changing {volume}");
        PlayerPrefs.SetInt(SOUND_EFFECTS_VOLUME, volume);
        return volume;
    }
    public int ChangeBGMVolume(bool increase)
    {
        Debug.Log($"Before changing {bgmVolume}");
        if(increase && bgmVolume < 10)
        {
            bgmVolume++;
            Debug.Log("Increased");
        }
        if(!increase && bgmVolume > 0)
        {
            bgmVolume--;
            Debug.Log("Decreased");
        }
            
        Debug.Log($"After changing {bgmVolume}");
        PlayerPrefs.SetInt(MUSIC_VOLUME, bgmVolume);
        return bgmVolume;
    }



    private void OnPlayerMovement(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipsRefsSO.footstep, player.transform.position);
    }

    private void OnObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipsRefsSO.objectDrop, trashCounter.transform.position);
    }

    private void OnObjectDrop(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipsRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void OnObjectPickup(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipsRefsSO.objectPickup, player.transform.position);
    }

    private void SuccessDelivery(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void FailDelivery(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipsRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void Chop(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipsRefsSO.chop, cuttingCounter.transform.position);
    }

    private void PlaySound(AudioClip[] clips, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], position, volume/10);
    }
}
