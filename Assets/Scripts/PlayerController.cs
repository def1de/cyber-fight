using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool doubleJump = true;
    private bool dash = true;
    private Vector3 velocity;
    private float XRotation;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private CharacterController Controller;
    [SerializeField] private Transform PlayerCamera;
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
        Controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        moveCamera();
    }

    private void MovePlayer()
    {
        // Movement
        Vector3 MoveDirection = transform.TransformDirection(PlayerMovementInput);
        // Jumping
        if (Controller.isGrounded && Input.GetButtonDown("Jump"))
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
            Controller.Move(dashDirection * Time.deltaTime);
        }

        // Gravity
        if (Controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime * -2f;
        }

        Controller.Move(MoveDirection * speed * Time.deltaTime);
        Controller.Move(velocity * Time.deltaTime);
    }

    private void moveCamera()
    {
        XRotation -= PlayerMouseInput.y * sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        PlayerCamera.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
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
        Controller.Move(direction * knockbackForce * 10 * Time.deltaTime);
    }
}
