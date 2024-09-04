using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] protected Transform counterTopPoint;
    public static event EventHandler OnObjectDrop;

    protected KitchenObject kitchenObject;
    public abstract void Interact(Player player);
    public abstract void InteractAlternate(Player player);
        public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public static void ClearStaticData()
    {
        OnObjectDrop = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnObjectDrop?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject!=null;
    }
}
