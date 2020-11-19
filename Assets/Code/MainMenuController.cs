using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int primarySceneIndex = -1;

    public void Play()
    {
        SceneManager.LoadScene(primarySceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
