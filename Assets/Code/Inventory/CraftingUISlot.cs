using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingUISlot : MonoBehaviour, IPointerDownHandler
{
    public Recipe recipe;
    public TextMeshProUGUI nameText;

    public void Enable(Recipe recipe)
    {
        this.recipe = recipe;
        this.nameText.text = recipe.resultName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(PlayerInventory.instance.TryCraftRecipe(recipe))
        {
            Destroy(gameObject);
        }
    }
}
