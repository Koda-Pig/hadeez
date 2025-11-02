using UnityEngine;

public interface IKitchenObjectParent
{
    public bool HasKitchenObject();
    public void ClearKitchenObject();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public Transform GetKitchenObjectFollowTransform();
}
