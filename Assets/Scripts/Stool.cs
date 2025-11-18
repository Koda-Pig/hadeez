using UnityEngine;

public class Stool : BaseCounter
{
    [SerializeField]
    GameObject playerVisual;

    [SerializeField]
    Transform sitPosition;
    private Vector3 previousPlayerPosition;
    private Quaternion previousPlayerRotation;

    public override void Interact(Player player)
    {
        if (player.IsSitting())
        {
            player.SetIsSitting(false);
            playerVisual.transform.position = previousPlayerPosition;
            playerVisual.transform.rotation = previousPlayerRotation;
        }
        else
        {
            // store current player position for later
            previousPlayerPosition = player.transform.position;
            previousPlayerRotation = player.transform.rotation;

            // make player sit
            player.SetIsSitting(true);
            playerVisual.transform.position = sitPosition.position;
            playerVisual.transform.rotation = sitPosition.rotation;
        }
    }
}
