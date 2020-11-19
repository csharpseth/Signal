using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    public float desiredYCoord = 20f;
    public GameObject uiPhone;
    public GameObject uiCallButton;
    public Image uiPhoneSignal;
    public AudioClip phoneHasSignalSFX;

    private bool audioTriggered = false;

    private void Update()
    {
        bool hasPhone = PlayerInventory.instance.HasItemQuantity(ItemIdentifier.Satellite_Phone, 1);
        uiPhone.SetActive(hasPhone);
        if (hasPhone == false) return;

        uiPhoneSignal.fillAmount = transform.position.y / desiredYCoord;

        if (transform.position.y >= desiredYCoord)
        {
            if(audioTriggered == false)
            {
                PlayerSoundController.instance.PlaySound(phoneHasSignalSFX);
                audioTriggered = true;
            }
            uiCallButton.SetActive(true);
        }
        else
        {
            uiCallButton.SetActive(false);
            audioTriggered = false;
        }
    }

    public void CompleteGame()
    {
        ScreenController.instance.Win();
    }
}
