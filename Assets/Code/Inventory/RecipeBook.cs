using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Custom/Crafting/Recipe Book")]
public class RecipeBook : ScriptableObject
{
    public Recipe[] recipes;

    public List<Recipe> GetCraftables(List<InventorySlot> inv)
    {
        List<Recipe> canCraft = new List<Recipe>();

        for (int i = 0; i < recipes.Length; i++)
        {
            int numRequirementsHad = 0;
            for (int j = 0; j < recipes[i].required.Length; j++)
            {
                ItemIdentifier reqID = recipes[i].required[j].id;
                int reqQuantity = recipes[i].required[j].quantity;

                for (int k = 0; k < inv.Count; k++)
                {
                    if (reqID == inv[k].id && inv[k].quantity >= reqQuantity)
                    {
                        numRequirementsHad++;
                    }
                }
            }
            if(numRequirementsHad >= recipes[i].required.Length)
            {
                canCraft.Add(recipes[i]);
            }
        }

        return canCraft;
    }
}

[System.Serializable]
public class Recipe
{
    public string resultName;
    public GameObject result;
    public RecipeRequirement[] required;
}

[System.Serializable]
public struct RecipeRequirement
{
    public ItemIdentifier id;
    public int quantity;
}