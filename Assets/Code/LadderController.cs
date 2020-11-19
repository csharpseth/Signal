using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    public Transform topOfLadder;
    public Transform startOfLadder;
    public Rigidbody ply;
    public float climbSpeed = 6f;

    private void FixedUpdate()
    {
        if (ply == null) return;

        ply.position = ply.position + (Vector3.up * climbSpeed);
        Debug.Log("Climbing?");
        if(ply.position.y >= topOfLadder.position.y)
        {
            ply.position = topOfLadder.position;
            ply.isKinematic = false;
            ply = null;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Interact"))
        {
            TryClimbLadder();
        }
    }

    private void TryClimbLadder()
    {
        ply = PlayerInventory.instance.GetComponent<Rigidbody>();
        ply.isKinematic = true;
        ply.position = startOfLadder.position;
    }
}
