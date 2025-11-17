using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }
    private List<RecipeSO> waitingRecipeSOList;

    [SerializeField]
    private RecipeListSO recipeListSO;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 8f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer > 0f)
        {
            return;
        }

        spawnRecipeTimer = spawnRecipeTimerMax;

        if (
            (waitingRecipeSOList.Count >= waitingRecipesMax)
            || !KitchenGameManager.Instance.IsGamePlaying()
        )
        {
            return;
        }

        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[
            UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)
        ];
        waitingRecipeSOList.Add(waitingRecipeSO);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            bool hasSameNumberOfIngredients =
                waitingRecipeSO.kitchenObjectSOList.Count
                == plateKitchenObject.GetKichenObjectSOList().Count;

            if (hasSameNumberOfIngredients)
            {
                bool plateContentsMatchesRecipe = true;

                foreach (
                    KitchenObjectSO recipeIngredient in waitingRecipeSO.kitchenObjectSOList
                )
                {
                    bool ingredientFound = false;

                    foreach (
                        KitchenObjectSO plateIngredient in plateKitchenObject.GetKichenObjectSOList()
                    )
                    {
                        if (plateIngredient == recipeIngredient)
                        {
                            //
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
                // Player delivered correct recipe
                if (plateContentsMatchesRecipe)
                {
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    successfulRecipesAmount++;
                    return;
                }
            }
        }
        // failure
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() => waitingRecipeSOList;

    public int GetSuccessfulRecipesAmount() => successfulRecipesAmount;
}
