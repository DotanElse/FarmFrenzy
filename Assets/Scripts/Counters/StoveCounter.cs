using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class StoveCounter : BaseCounter, IHasProgress
{
    [SerializeField] private FryingRecipeSO[] recipes;
    
    public enum State{
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;
    private float fryingTime;
    private float burningTime;
    private FryingRecipeSO currRecipe;

    public event EventHandler<IHasProgress.OnProgressChangeArgs> OnProgressChange;
    public event EventHandler<OnStateChangeArgs> OnStateChange;

    public class OnStateChangeArgs : EventArgs
    {
        public State state;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
    }

    // Update is called once per frame
    void Update()
    {
        if(!HasKitchenObject())
            return;
        switch(state)
        {
            case State.Idle:
                break;
            case State.Burned:
                break;
            case State.Frying:
                fryingTime += Time.deltaTime;
                OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                    normalizedProgress = fryingTime/currRecipe.fryingRequired
                    });
                if(fryingTime > currRecipe.fryingRequired)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(currRecipe.output, this);
                    currRecipe = GetRecipe(currRecipe.output);
                    if(currRecipe != null)
                    {
                        burningTime = 0;
                        state = State.Fried;
                        OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
                    }
                    else
                    {
                        state = State.Burned;
                        OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
                    }
                }
                break;
            case State.Fried:
                burningTime += Time.deltaTime;
                OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                    normalizedProgress = burningTime/currRecipe.fryingRequired
                    });
                if(burningTime > currRecipe.fryingRequired)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(currRecipe.output, this);
                    state = State.Burned;
                    OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
                }
                break;
        }
    }
    override public void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                //place item on counter
                if(HasRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    currRecipe = GetRecipe(GetKitchenObject().GetKitchenObjectSO());
                    fryingTime = 0;
                    OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                        normalizedProgress = 0f
                    });
                    state = State.Frying;
                    OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
                } 
            }
        }
        else
        {
            if(!player.HasKitchenObject())
            {
                //pick item from counter
                GetKitchenObject().SetKitchenObjectParent(player);
                OnProgressChange?.Invoke(this,  new IHasProgress.OnProgressChangeArgs{
                    normalizedProgress = 0f
                });
                state = State.Idle;
                OnStateChange?.Invoke(this, new OnStateChangeArgs{state = this.state});
            }
        }
    }
    public override void InteractAlternate(Player player){}

    private bool HasRecipe(KitchenObjectSO item)
    {
        return GetFryResult(item) != null;
    }

    private FryingRecipeSO GetRecipe(KitchenObjectSO item)
    {
        foreach (FryingRecipeSO recipe in recipes)
        {
            if(recipe.input == item)
                return recipe;
        }
        return null;
    }

    private KitchenObjectSO GetFryResult(KitchenObjectSO item)
    {
        foreach (FryingRecipeSO recipe in recipes)
        {
            if(recipe.input == item)
                return recipe.output;
        }
        return null;
    }
}
