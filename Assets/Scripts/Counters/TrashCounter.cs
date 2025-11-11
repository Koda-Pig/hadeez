using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            return;
        }
        player.GetKitchenObject().DestroySelf();
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }

    public static new void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
}
