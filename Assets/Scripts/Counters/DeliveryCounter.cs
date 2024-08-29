using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    override public void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
    override public void InteractAlternate(Player player) {}
}
