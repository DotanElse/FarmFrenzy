using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private float spawnPlateTime = 3f;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlateRemoved;
    public event EventHandler OnPlateAdded;
    private int maxPlates = 4;
    private float plateTimer;
    private int platesAmount;

    void Start()
    {
        plateTimer = 0;
        platesAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        plateTimer += Time.deltaTime;
        if(plateTimer > spawnPlateTime)
        {
            if(platesAmount < maxPlates)
            {
                Debug.Log("Spawned");
                OnPlateAdded?.Invoke(this, EventArgs.Empty);
                platesAmount++;
            }
            plateTimer = 0;
        }
    }

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
            return;
        if(platesAmount > 0)
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            platesAmount--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void InteractAlternate(Player player) {}

}
