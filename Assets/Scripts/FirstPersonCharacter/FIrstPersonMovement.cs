using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {

    public CharacterController controller;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    public float speed = 4.5f;
    public float sprint = 6f;
    public float gravity = -12f;
    public float jumpHeight = 1.5f;

    Vector3 velocity;
    bool isGrounded;

    void Update() {

        // Falling
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) { 
            velocity.y = -2f;
        }

        // Sprinting
        float movementSpeed;
        if (Input.GetButton("Sprint") && isGrounded) {
            movementSpeed = sprint;
        } else {
            movementSpeed = speed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * movementSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}