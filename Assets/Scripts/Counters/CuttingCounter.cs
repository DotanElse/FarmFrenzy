using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    private int cuttingProgress;
    [SerializeField] private CuttingRecipeSO[] recipes;
    public event EventHandler OnCut;
    public static event EventHandler OnAnyCut;
    public event EventHandler<IHasProgress.OnProgressChangeArgs> OnProgressChange;

    
    public static new void ClearStaticData()
    {
        OnAnyCut = null;
    }


    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                cuttingProgress = 0;
                if(HasRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                    normalizedProgress = 0.0001f
                    });
                }
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if(!player.HasKitchenObject())
            {
                //give to player
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                //full counter and player has an object in hand
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        //ingredient added
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
        if(HasKitchenObject())
        {
            KitchenObjectSO output = GetCutResult(GetKitchenObject().GetKitchenObjectSO());
            if(output == null)
                return;
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            int requiredCuts = GetRequiredCuts(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                normalizedProgress = (float)cuttingProgress/requiredCuts
            });

            if(cuttingProgress == requiredCuts)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(output, this);
            }
        }
    }

    private bool HasRecipe(KitchenObjectSO item)
    {
        return GetCutResult(item) != null;
    }

    private int GetRequiredCuts(KitchenObjectSO item)
    {
        foreach (CuttingRecipeSO recipe in recipes)
        {
            if(recipe.input == item)
                return recipe.cutsRequired;
        }
        return 0;
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
