using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance;

    public GameObject pauseScreen;

    private MovementController moveController;
    private PlayerInventory inventory;
    private PlayerStats stats;
    private PlayerMouseController mouse;

    private bool stopped = false;

    private GameObject lastLookedAt;
    private PromptObject lastPrompted;
    private bool tabOut = false;

    public void Stop(bool stop = true)
    {
        stopped = stop;
        mouse.Lock(!stopped);
    }

    private void Awake()
    {
        instance = this;
        moveController = GetComponent<MovementController>();
        inventory = GetComponent<PlayerInventory>();
        stats = GetComponent<PlayerStats>();
        mouse = GetComponent<PlayerMouseController>();
    }

    private void Update()
    {
        if (stopped) return;

        if (moveController == null) return;

        if(PlayerMouseController.instance.LookingAt() != lastLookedAt)
        {
            if(lastPrompted != null)
                lastPrompted.ClearPrompt();

            lastPrompted = PlayerMouseController.instance.LookingAt().GetComponent<PromptObject>();
            if (lastPrompted != null)
                lastPrompted.Prompt();
        }


        moveController.Move(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButton("Sprint"));

        if(Input.GetButtonDown("Interact"))
        {
            inventory.TryCollectItem();
        }

        if(Input.GetButtonDown("Consume"))
        {
            stats.TryConsumeResource();
        }

        if(Input.GetButtonDown("Inventory"))
        {
            inventory.Toggle();
            mouse.Lock(!inventory.Active);

        }

        if(Input.GetButtonDown("Cancel"))
        {
            tabOut = !tabOut;
            mouse.Lock(!tabOut);
            pauseScreen.SetActive(tabOut);
        }

        if (Input.GetButtonDown("Jump"))
            moveController.Jump();
    }
}
