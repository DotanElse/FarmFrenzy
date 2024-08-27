using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    private int cuttingProgress;
    [SerializeField] private CuttingRecipeSO[] recipes;
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangeArgs> OnProgressChange;



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
                this.GetKitchenObject().SetKitchenObjectParent(player);
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
