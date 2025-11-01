using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO kicthenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO() => kicthenObjectSO;
}
