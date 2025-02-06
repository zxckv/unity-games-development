using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool jumpReady = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool onGround;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    private void Update() {
        onGround = Physics.Raycast(transform.position, Vector3.down, 0.1f, ground);

        UserInput();
        ControlSpeed();

        if (onGround) { 
            rigidBody.drag = groundDrag; 
        } else {
            rigidBody.drag = 0;
        }
    }

    private void FixedUpdate() {
        PlayerMove();
    }

    private void UserInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && jumpReady && onGround) {
            jumpReady = false;
            PlayerJump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void PlayerMove() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (onGround) {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (!onGround) {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void ControlSpeed() {
        Vector3 flatVelocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVelocity.magnitude > moveSpeed) { 
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVelocity.x, rigidBody.velocity.y, limitedVelocity.z);
        }
    }

    private void PlayerJump() {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        jumpReady = true;
    }

}
