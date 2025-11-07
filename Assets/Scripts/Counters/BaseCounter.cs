using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public static event EventHandler OnAnyObjectPlacedHere;

    public virtual void Interact(Player player)
    {
        Debug.LogError(
            "this shouldn't be getting called. Each counter should have its own implementation of `Interact`"
        );
    }

    public virtual void InteractAlternate(Player player)
    {
        // Not all counters need an alternate interact method.
    }

    public bool HasKitchenObject() => kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject == null)
        {
            return;
        }
        OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
