using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptObject : MonoBehaviour
{
    [TextArea]
    public string prompt = "";

    private void OnDestroy()
    {
        if (UIPromptController.currentlyPrompting == this) 
        {
            UIPromptController.SetPrompt("", false, null);
        }
    }

    public void Prompt()
    {
        UIPromptController.SetPrompt(prompt, true, this);
        UIPromptController.currentlyPrompting = this;
    }

    public void ClearPrompt()
    {
        UIPromptController.SetPrompt("", false, null);
        UIPromptController.currentlyPrompting = null;
    }
}
