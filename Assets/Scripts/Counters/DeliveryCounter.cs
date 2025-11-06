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
        if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject _))
        {
            return;
        }

        player.GetKitchenObject().DestroySelf();
    }
}
