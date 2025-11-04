using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private CuttingRecipeSO[] cutKitchenObjectSOs;

    private int cuttingProgress;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject()) // player is carrying something
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) // player is carrying something that can be cut
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(
                        GetKitchenObject().GetKitchenObjectSO()
                    );

                    float progressPercentage =
                        (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;

                    OnProgressChanged?.Invoke(
                        this,
                        new OnProgressChangedEventArgs { progressNormalized = progressPercentage }
                    );
                }
            }
            else
            {
                // player not carrying anything
            }
        }
        else
        {
            Debug.Log("kitchenobject here!");
            Debug.Log(GetKitchenObject());

            if (player.HasKitchenObject())
            {
                // player is carrying something
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        KitchenObjectSO objectOnCounter = GetKitchenObject().GetKitchenObjectSO();

        if (HasKitchenObject() && HasRecipeWithInput(objectOnCounter)) // only continue if there is a kitchen object that can be cut
        {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(objectOnCounter);

            float progressPercentage = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;

            OnProgressChanged?.Invoke(
                this,
                new OnProgressChangedEventArgs { progressNormalized = progressPercentage }
            );

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(objectOnCounter);
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cutKitchenObjectSOs)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }

        return null;
    }
}
