using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerMouseController : MonoBehaviour
{
    public static PlayerMouseController instance;
    public float lookSpeed = 10f;
    public float maxHorizontalAngle = 60f;
    public float airbornMultipler = 0.25f;
    private Camera cam;
    private Transform playerBody;
    private Transform playerCameraTransform;
    private MovementController moveController;
    private new bool enabled = true;


    public GameObject LookingAt()
    {
        Ray r = cam.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;
        if(Physics.Raycast(r, out hit, 10f))
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    private void Awake()
    {
        instance = this;
        cam = GetComponentInChildren<Camera>();
        moveController = GetComponent<MovementController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(playerCameraTransform == null)
        {
            playerCameraTransform = GetComponentInChildren<Camera>().transform;
        }

        if (playerBody == null)
            playerBody = transform;
    }

    public void Lock(bool locked)
    {
        this.enabled = locked;

        if(locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (enabled == false) return;

        float h = lookSpeed * Input.GetAxisRaw("Mouse X");
        float v = -lookSpeed * Input.GetAxisRaw("Mouse Y");

        if (moveController.IsGrounded == false)
            h *= airbornMultipler;

        Vector3 bodyRot = playerBody.eulerAngles;
        bodyRot.y += h;
        playerBody.eulerAngles = bodyRot;

        Vector3 camRot = playerCameraTransform.localEulerAngles;

        camRot.x += v;

        if (camRot.x > 0f && camRot.x < 180f && camRot.x > maxHorizontalAngle)
        {
            camRot.x = maxHorizontalAngle;
        }
        if (camRot.x > 0f && camRot.x > 180f && camRot.x < (360f - maxHorizontalAngle))
            camRot.x = (360f - maxHorizontalAngle);

        playerCameraTransform.localEulerAngles = camRot;
    }
}
