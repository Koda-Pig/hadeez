using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectSO> kitchenObjectSOList;

    [SerializeField]
    private List<KitchenObjectSO> validKitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (
            kitchenObjectSOList.Contains(kitchenObjectSO) // already has one of these ingredients on the plate
            || !validKitchenObjectSOList.Contains(kitchenObjectSO) // invalid ingredient
        )
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }
}
