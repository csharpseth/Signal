using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerCrafting : MonoBehaviour
{
    private PlayerInventory inventory;
    public RecipeBook recipes;
    public GameObject uiCraftSlotPrefab;
    public Transform uiCraftContainer;


    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        inventory.OnInventoryUpdated += OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {
        for (int i = 0; i < uiCraftContainer.childCount; i++)
        {
            Destroy(uiCraftContainer.GetChild(i).gameObject);
        }

        List<Recipe> canCrafts = recipes.GetCraftables(inventory.content);
        for (int i = 0; i < canCrafts.Count; i++)
        {
            CraftingUISlot slot = Instantiate(uiCraftSlotPrefab, uiCraftContainer).GetComponent<CraftingUISlot>();
            slot.Enable(canCrafts[i]);
        }
    }

}
