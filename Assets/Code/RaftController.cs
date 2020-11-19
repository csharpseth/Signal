using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public Vector3 raftDestination;
    public Vector3 raftRotation;
    public AudioClip arrivalSound;

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Interact"))
        {
            TryRideRaft();
        }
    }

    private void TryRideRaft()
    {
        if (PlayerInventory.instance.HasItemQuantity(ItemIdentifier.Paddle, 1) == false)
        {
            Debug.Log("You Need A Paddle");
            return;
        }

        if(raftDestination != null)
        {
            transform.position = raftDestination;
            transform.eulerAngles = raftRotation;
            PlayerInventory.instance.transform.position = transform.position + Vector3.up;
            PlayerSoundController.instance.PlaySound(arrivalSound);
        }
    }
}
