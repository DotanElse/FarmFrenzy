using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioClipsRefsSO audioClipsRefsSO; 
    public static SoundManager Instance {get; private set;}
    int volume = 5;

    void Awake()
    {
        Instance = this;
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
        return volume;
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
