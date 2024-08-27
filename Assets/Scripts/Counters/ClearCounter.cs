using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    override public void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                //empty counter and player wanna place something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //empty counter and player empty
            }
        }
        else
        {
            if(!player.HasKitchenObject())
            {
                //full counter and player doesnt have an object in hand
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                //full counter and player has an object in hand
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player holding a plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //ingredient added to player's plate
                        GetKitchenObject().DestroySelf();
                        return;
                    }
                }
                if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {
                    //counter has a plate on it
                    if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //ingredient added to counter's plate
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
            
        }
    }
    public override void InteractAlternate(Player player)
    {
        Debug.Log("alt interacting with clear");
    }
}
