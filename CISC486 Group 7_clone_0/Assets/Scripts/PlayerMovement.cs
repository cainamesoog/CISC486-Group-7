using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PurrNet;
using TMPro;

public class PlayerMovement : NetworkIdentity
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;

    public float groundDrag = 6f;

    public float jumpForce = 5f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.5f;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground/Collider")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float coyoteTimer = 0f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        readyToJump = true;

        moveSpeed = walkSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();

        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        bool effectiveGrounded = coyoteTimer > 0f;

        StateHandler(effectiveGrounded);

        SpeedControl();

        float targetDrag = effectiveGrounded ? groundDrag : 0f;
        rb.drag = Mathf.Lerp(rb.drag, targetDrag, Time.deltaTime * 8f);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && (coyoteTimer > 0f))
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyUp(sprintKey))
        {
            if (state == MovementState.sprinting)
            {
                moveSpeed = walkSpeed;
                state = MovementState.walking;
            }
        }
    }

    private void StateHandler(bool effectiveGrounded)
    {
        if (effectiveGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (effectiveGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (coyoteTimer > 0f)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        if (horizontalInput == 0 && verticalInput == 0 && coyoteTimer > 0f)
        {
            Vector3 damp = new Vector3(rb.velocity.x, 0f, rb.velocity.z) * 0.9f;
            rb.velocity = new Vector3(damp.x, rb.velocity.y, damp.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        coyoteTimer = 0f;
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EvaluateCollisionForGround(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        EvaluateCollisionForGround(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsGround) != 0)
        {
            grounded = false;
        }
    }

    private void EvaluateCollisionForGround(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsGround) == 0) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                grounded = true;
                coyoteTimer = coyoteTime;
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rb != null)
        {
            Gizmos.color = (coyoteTimer > 0f) ? Color.green : Color.red;
            Vector3 pos = transform.position + Vector3.down * (playerHeight * 0.5f - 0.1f);
            Gizmos.DrawWireSphere(pos, 0.2f);
        }
    }
}