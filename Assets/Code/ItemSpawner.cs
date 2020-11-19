using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static bool satPhoneSpawned = false;

    public ItemDatabase database;

    private void Awake()
    {
        int pick = Random.Range(0, database.items.Count);
        if(database.items[pick].id == ItemIdentifier.Satellite_Phone && satPhoneSpawned == false)
        {
            Instantiate(database.items[pick].prefab, transform.position, transform.rotation);
            satPhoneSpawned = true;
        }else if(database.items[pick].id != ItemIdentifier.Satellite_Phone)
        {
            Instantiate(database.items[pick].prefab, transform.position, transform.rotation);
        }
        
    }
}
