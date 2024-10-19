using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private bool doubleJump = true;
    private bool dash = true;
    private Vector3 velocity;
    private float xRotation;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private CharacterController controller;
    [FormerlySerializedAs("PlayerCamera")] [SerializeField] private Transform playerCamera;
    [Space]
    [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float sensitivity;
    [SerializeField] private float gravity = -9.81f;
    [Header("External Forces Settings")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float knockbackForce;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();
    }

    private void MovePlayer()
    {
        // Movement
        Vector3 moveDirection = transform.TransformDirection(playerMovementInput);
        // Jumping
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            doubleJump = true;
            velocity.y = jumpForce;
        }
        else if (doubleJump && Input.GetButtonDown("Jump"))
        {
            doubleJump = false;
            velocity.y = jumpForce;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash)
        {
            dash = false;
            Vector3 dashDirection = transform.forward * dashForce * 10;
            Invoke("ResetDash", dashCooldown);
            controller.Move(dashDirection * Time.deltaTime);
        }

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime * -2f;
        }

        controller.Move(moveDirection * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    private void MoveCamera()
    {
        xRotation -= playerMouseInput.y * sensitivity;

        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void ResetDash()
    {
        dash = true;
    }
    public void Knockback(GameObject entity)
    {
        Vector3 direction = this.transform.position - entity.transform.position;
        direction.y = 0.1f;
        direction.Normalize();
        controller.Move(direction * knockbackForce * 10 * Time.deltaTime);
    }
}
