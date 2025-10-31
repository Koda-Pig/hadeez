using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private Transform tomatoPrefab;

    [SerializeField]
    private Transform cheesePrefab;

    [SerializeField]
    private Transform counterTopPoint;

    public void Interact()
    {
        Transform tomatoTransform = Instantiate(tomatoPrefab, counterTopPoint);
        tomatoTransform.localPosition = Vector3.zero;
    }
}
