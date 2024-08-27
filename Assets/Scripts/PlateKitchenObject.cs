using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> allowedItems;
    private List<KitchenObjectSO> itemsOnPlate;
    public event EventHandler<OnIngredientAddArgs> OnIngredientAdd;

    public class OnIngredientAddArgs : EventArgs
    {
        public KitchenObjectSO ingredient;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        itemsOnPlate = new List<KitchenObjectSO>();
    }

    // Update is called once per frame
    public bool TryAddIngredient(KitchenObjectSO item)
    {
        if(!allowedItems.Contains(item))
            return false;
        if(!itemsOnPlate.Contains(item))
        {
            //new item on plate
            itemsOnPlate.Add(item);
            OnIngredientAdd?.Invoke(this, new OnIngredientAddArgs{
                ingredient = item
            });
            return true; 
        }
        return false;
    }
}
