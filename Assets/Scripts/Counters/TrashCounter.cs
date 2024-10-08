using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashed;

    public static new void ClearStaticData()
    {
        OnObjectTrashed = null;
    }
    override public void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
    override public void InteractAlternate(Player player) {}
}
