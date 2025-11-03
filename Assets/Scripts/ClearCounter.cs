using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            Debug.Log("no kitchenobject here");

            if (player.HasKitchenObject())
            {
                Debug.Log("THe player is carrying something");
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // player not carrying anything
            }
        }
        else
        {
            Debug.Log("kitchenobject here!");
            Debug.Log(GetKitchenObject());

            if (player.HasKitchenObject())
            {
                // player is carrying something
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
