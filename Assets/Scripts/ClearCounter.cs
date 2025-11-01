using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    [SerializeField]
    private Transform counterTopPoint;

    public void Interact()
    {
        Transform ktichenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        ktichenObjectTransform.localPosition = Vector3.zero;

        Debug.Log(
            ktichenObjectTransform.GetComponent<KitchenObject>()?.GetKitchenObjectSO().objectName
        );
    }
}
