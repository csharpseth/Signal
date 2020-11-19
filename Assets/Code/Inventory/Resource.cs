using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour
{
    public string identifier;
    public int value;

    public void Consume()
    {
        Destroy(gameObject);
    }
}