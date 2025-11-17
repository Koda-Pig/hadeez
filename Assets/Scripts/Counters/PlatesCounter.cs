using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    [SerializeField]
    private KitchenObjectSO plateKitchenObjectSO;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;

            if (
                platesSpawnedAmount >= platesSpawnedAmountMax
                || !KitchenGameManager.Instance.IsGamePlaying()
            )
            {
                return;
            }

            platesSpawnedAmount++;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Interact(Player player)
    {
        // player already holding something
        if (player.HasKitchenObject())
        {
            return;
        }

        // there's no plates available
        if (platesSpawnedAmount == 0)
        {
            return;
        }

        platesSpawnedAmount--;
        KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
        OnPlateRemoved?.Invoke(this, EventArgs.Empty);
    }
}
