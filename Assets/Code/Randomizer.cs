using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public float minScale = 1.5f;
    public float maxScale = 3f;

    private void Awake()
    {
        transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
    }
}
