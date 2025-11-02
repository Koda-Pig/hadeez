using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    [SerializeField]
    private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Transform ktichenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            ktichenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            // Player pickup object
            kitchenObject.SetKitchenObjectParent(player);
        }
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
