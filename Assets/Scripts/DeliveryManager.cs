using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance {get; private set;}
    [SerializeField] private RecipeListSO recipes;
    private List<RecipeSO> waitingRecipes;
    private float spawnRecipeTime = 4f;
    private float recipeTimer = 0f;
    private int maxRecipeCount = 6;
    void Awake()
    {
        Instance = this;
        waitingRecipes = new List<RecipeSO>();
    }
    // Update is called once per frame
    void Update()
    {
        if(recipeTimer < spawnRecipeTime)
            recipeTimer+= Time.deltaTime;
        //max amount reached
        if(waitingRecipes.Count >= maxRecipeCount)
        {
            return;
        }
        if(recipeTimer >= spawnRecipeTime)
        {
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            recipeTimer = 0;
            waitingRecipes.Add(recipes.recipeList[UnityEngine.Random.Range(0, recipes.recipeList.Count)]);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> plateItems = plateKitchenObject.GetItemsOnPlate();
        foreach(KitchenObjectSO item in plateItems)
            {
                Debug.Log($"Plate contained before delivery {item.name}");
            }
        for(int i=0; i<waitingRecipes.Count; i++)
        {
            List<KitchenObjectSO> recipeItems = waitingRecipes[i].items;
            bool areEqual = plateItems.Count == recipeItems.Count &&
                        plateItems.TrueForAll(item => recipeItems.Contains(item)) &&
                        recipeItems.TrueForAll(item => plateItems.Contains(item));
            if(areEqual)
            {
                Debug.Log("Delivered successfully");
                foreach(KitchenObjectSO item in plateItems)
                {
                    Debug.Log($"Plate contained {item.name}");
                }
                waitingRecipes.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
        Debug.Log("No waiting recipe match");
    }

    public List<RecipeSO> GetWaitingRecipes()
    {
        return waitingRecipes;
    }
}
