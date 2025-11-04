using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Interact(); // this shouldn't be getting called");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("BaseCounter.InteractAlternate(); // this shouldn't be getting called");
    }

    public bool HasKitchenObject() => kitchenObject != null;

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
