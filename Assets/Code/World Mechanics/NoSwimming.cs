using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSwimming : MonoBehaviour
{
    public float waterLevel = 10f;
    public Transform camera;

    private void Update()
    {
        if(camera.position.y <= waterLevel)
        {
            ScreenController.instance.Die();
        }
    }
}
