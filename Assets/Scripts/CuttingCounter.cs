using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] recipes;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if(!player.HasKitchenObject())
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            Debug.Log("Happening");
            KitchenObjectSO output = GetCutResult(GetKitchenObject().GetKitchenObjectSO());
            if(output == null)
                return;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(output, this);
        }
    }

    private bool HasRecipe(KitchenObjectSO item)
    {
        return GetCutResult(item) == null;
    }

    private KitchenObjectSO GetCutResult(KitchenObjectSO item)
    {
        foreach (CuttingRecipeSO recipe in recipes)
        {
            if(recipe.input == item)
                return recipe.output;
        }
        return null;
    }
}
