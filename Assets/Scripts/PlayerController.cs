using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float playerHeight = 1f;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float XRotation;
    private float YRotation;
    public float jumpCooldown;
    public float dashCooldown;
    public float dashForce;
    bool readyToJump = true;
    bool readyToDash = true;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    public bool grounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.1f, whatIsGround);
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        movePlayer();
        moveCamera();
    }

    private void movePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && readyToDash)
        {
            readyToDash = false;
            Debug.Log("Dash");
            PlayerBody.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    private void ResetDash()
    {
        readyToDash = true;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void moveCamera()
    {
        XRotation -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
    }
}
