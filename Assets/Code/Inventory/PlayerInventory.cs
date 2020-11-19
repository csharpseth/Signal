using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public GameObject uiSlotPrefab;
    public Transform uiContainer;
    public GameObject inventoryCanvas;

    public AudioClip openSound;
    public AudioClip closeSound;

    public ItemDatabase database;
    public List<InventorySlot> content;
    public Action OnInventoryUpdated;

    public bool Active => inventoryCanvas.activeSelf;
    public Vector3 dropPos;

    private void Update()
    {

        dropPos = transform.position + transform.forward + transform.up;

    }

    private void Awake()
    {
        instance = this;
        inventoryCanvas.SetActive(false);
    }

    public bool TryCollectItem()
    {
        Item item = PlayerMouseController.instance.LookingAt().GetComponent<Item>();
        if (item == null) return false;

        CollectItem(item);

        return true;
    }

    private void CollectItem(Item item)
    {
        ItemIdentifier id = item.Collect();
        GameObject itemPrefab = database.GetItemPrefab(id);

        for (int i = 0; i < content.Count; i++)
        {
            if(content[i].id == id)
            {
                content[i].Increase();
                OnInventoryUpdated?.Invoke();
                return;
            }
        }

        InventoryUISlot slot = Instantiate(uiSlotPrefab, uiContainer).GetComponent<InventoryUISlot>();
        slot.Enable(id, 1, itemPrefab);
        content.Add(new InventorySlot(id, 1, slot));
        Destroy(item.gameObject, 0.1f);

        OnInventoryUpdated?.Invoke();
    }

    public void Toggle()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        if(inventoryCanvas.activeSelf)
        {
            PlayerSoundController.instance.PlaySound(openSound);
        }
        else
        {
            PlayerSoundController.instance.PlaySound(closeSound);
        }
    }

    public bool AdjustQuantityOfItem(ItemIdentifier id, int adjustment, bool dropIfNegative = true)
    {
        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].id == id)
            {
                if(content[i].quantity + adjustment < 0)
                {
                    return false;
                }


                if(adjustment < 0 && dropIfNegative)
                {
                    for (int j = 0; j < Mathf.Abs(adjustment); j++)
                    {
                        Resource r  = Instantiate(content[i].ui.itemPrefab, dropPos, Quaternion.identity).GetComponent<Resource>();
                        if(r != null)
                        {
                            PlayerStats.instance.ConsumeResource(r);
                        }
                    }
                }

                content[i].quantity += adjustment;

                if(content[i].quantity <= 0)
                {
                    Destroy(content[i].ui.gameObject);
                    content.RemoveAt(i);
                    return true;
                }


                content[i].ui.UpdateData(content[i].quantity);
                return true;
            }
        }

        return false;
    }

    public bool HasItemQuantity(ItemIdentifier id, int quantity)
    {
        for (int i = 0; i < content.Count; i++)
        {
            if(content[i].id == id && content[i].quantity >= quantity)
            {
                return true;
            }
        }

        return false;
    }

    public bool TryCraftRecipe(Recipe r)
    {
        for (int i = 0; i < r.required.Length; i++)
        {
            if(HasItemQuantity(r.required[i].id, r.required[i].quantity) == false)
            {
                return false;
            }
        }

        Instantiate(r.result, dropPos + (transform.forward * 2f) + transform.up, Quaternion.identity);

        for (int i = 0; i < r.required.Length; i++)
        {
            AdjustQuantityOfItem(r.required[i].id, -Mathf.Abs(r.required[i].quantity), false);
        }

        return true;
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemIdentifier id;
    public int quantity;
    public InventoryUISlot ui;

    public InventorySlot(ItemIdentifier id, int quantity, InventoryUISlot ui)
    {
        this.id = id;
        this.quantity = quantity;
        this.ui = ui;
    }

    public void Increase()
    {
        quantity++;
        ui.UpdateData(quantity);
    }

    public void Decrease()
    {
        quantity--;
        ui.UpdateData(quantity);
    }
}

public enum ItemIdentifier
{
    Satellite_Phone,
    Logs,
    Rope,
    Energy_Bar,
    Water_Bottle,
    Paddle
}
