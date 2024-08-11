using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    override public void Interact(Player player)
    {
        if(player.HasKitchenObject())
            return;
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player); 
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
