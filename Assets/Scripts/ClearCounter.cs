using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    override public void Interact(Player player)
    {

        if(kitchenObject == null)
        {    
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); 
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
}
