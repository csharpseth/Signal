using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Custom/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<DatabaseElement> items;

    public GameObject GetItemPrefab(ItemIdentifier id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].id == id)
            {
                return items[i].prefab;
            }
        }

        return null;
    }
}

[System.Serializable]
public class DatabaseElement
{
    public ItemIdentifier id;
    public GameObject prefab;
}