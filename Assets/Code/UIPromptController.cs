using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPromptController : MonoBehaviour
{
    private static string content = "";
    private static bool active = false;
    public static PromptObject currentlyPrompting;
    public GameObject promptObject;
    public TextMeshProUGUI prompt;

    private void Awake()
    {
        if (promptObject == null) promptObject = prompt.gameObject;
    }

    private void Update()
    {
        promptObject.SetActive(active);

        prompt.text = content;
    }

    public static void SetPrompt(string content, bool active, PromptObject po)
    {
        UIPromptController.content = content;
        UIPromptController.active = active;
        currentlyPrompting = po;
    }
}
