using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField]
    private CuttingRecipeSO[] cutKitchenObjectSOs;

    private int cuttingProgress;
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

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
                        new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = progressPercentage,
                        }
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
            // player is carrying something
            if (player.HasKitchenObject())
            {
                if (
                    player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)
                )
                {
                    if (
                        plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())
                    )
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!GetKitchenObject())
        {
            Debug.Log("There's nothing on the counter to interact with");
            return;
        }

        KitchenObjectSO objectOnCounter = GetKitchenObject().GetKitchenObjectSO();

        if (HasKitchenObject() && HasRecipeWithInput(objectOnCounter)) // only continue if there is a kitchen object that can be cut
        {
            cuttingProgress++;

            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(objectOnCounter);

            float progressPercentage = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;

            OnProgressChanged?.Invoke(
                this,
                new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = progressPercentage,
                }
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
