using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public static Vector3 position;

    public float walkSpeed = 6f;
    public float runSpeed = 11f;
    public float jumpForce = 300f;
    public float airbornStrafeSpeedMultiplier = 0.25f;
    public float groundCheckRadius = 0.25f;
    public LayerMask groundMask;

    public AudioClip[] stepSounds;
    public float minStepDelay = 0.5f;
    public float maxStepDelay = 1.25f;
    private float stepTime = 0f;
    private float currentStepDelay => ((maxStepDelay - minStepDelay) * (1f - (speed / runSpeed))) + minStepDelay;

    private Rigidbody rb;
    private Vector2 inputDir;
    private Vector2 lastInputDir;
    private Vector3 velocity;
    private bool grounded;
    private float speed;
    private float lastSpeed;

    public bool IsGrounded => grounded;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        position = transform.position;
        grounded = Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);

        if(inputDir != Vector2.zero && grounded)
        {
            stepTime += Time.deltaTime;
            if(stepTime >= currentStepDelay)
            {
                Step();
                stepTime = 0f;
            }
        }
        else
        {
            stepTime = currentStepDelay / 2f;
        }
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        velocity = Vector3.zero;
        velocity += transform.forward * inputDir.y * speed;
        velocity += transform.right * inputDir.x * speed;


        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    public void Move(float horizontal, float vertical, bool sprint)
    {
        inputDir.x = horizontal;
        inputDir.y = vertical;

        speed = (sprint ? runSpeed : walkSpeed);
    }

    public void Step()
    {
        PlayerSoundController.instance.PlaySound(stepSounds[Random.Range(0, stepSounds.Length)]);
    }

    public void Jump()
    {
        if (grounded == false) return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        lastInputDir = inputDir;
        lastSpeed = speed;
        grounded = false;
    }
}
