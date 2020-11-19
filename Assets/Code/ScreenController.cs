using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public static ScreenController instance;
    public GameObject deathScreenCanvas;
    public GameObject winScreenCanvas;
    private void Awake()
    {
        instance = this;
        deathScreenCanvas.SetActive(false);
        winScreenCanvas.SetActive(false);
    }



    public void Win()
    {
        winScreenCanvas.SetActive(true);
        PlayerInputController.instance.Stop();
    }

    public void Die()
    {
        deathScreenCanvas.SetActive(true);
        PlayerInputController.instance.Stop();
    }
}
