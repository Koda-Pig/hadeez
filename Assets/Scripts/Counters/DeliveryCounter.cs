using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            return;
        }

        // Player is not holding a plate
        if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            return;
        }

        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

        player.GetKitchenObject().DestroySelf();
    }
}
