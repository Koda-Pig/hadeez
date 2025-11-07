using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    private List<RecipeSO> waitingRecipeSOList;

    [SerializeField]
    private RecipeListSO recipeListSO;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

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

        if (waitingRecipeSOList.Count >= waitingRecipesMax)
        {
            return;
        }

        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[
            UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)
        ];
        waitingRecipeSOList.Add(waitingRecipeSO);

        Debug.Log(waitingRecipeSO.recipeName);
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

                foreach (KitchenObjectSO recipeIngredient in waitingRecipeSO.kitchenObjectSOList)
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
                if (plateContentsMatchesRecipe)
                {
                    // Player delivered correct recipe
                    Debug.Log("player delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        // no matches found, meaning player delivered a recipe not found on the waiting list

        Debug.Log("player did not deliver a correct recipe");
    }
}
