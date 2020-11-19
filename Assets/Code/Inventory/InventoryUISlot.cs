using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour, IPointerDownHandler
{
    public Image icon;
    public TextMeshProUGUI identifier;
    public TextMeshProUGUI quantity;
    public string quantityFormat = "x{0}";
    private ItemIdentifier id;
    public GameObject itemPrefab;

    public void Enable(ItemIdentifier identifier, int initialQuantity, GameObject itemPrefab)
    {
        this.itemPrefab = itemPrefab;
        id = identifier;
        this.identifier.text = identifier.ToString().Replace('_', ' ');
        quantity.text = string.Format(quantityFormat, initialQuantity);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void UpdateData(int quantity)
    {
        this.quantity.text = string.Format(quantityFormat, quantity);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerInventory.instance.AdjustQuantityOfItem(id, -1);
        Debug.Log("Drop Item");
    }
}
