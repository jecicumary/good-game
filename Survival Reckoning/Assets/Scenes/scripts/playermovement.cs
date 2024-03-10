using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float superSonicSpeed = 10.0f;
    public float jumpForce = 5.0f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100.0f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float verticalLookRotation;
    private Transform cameraTransform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check if the player is grounded
        bool isGrounded = controller.isGrounded;

        // Declare isGrounded here
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Apply speed based on whether the player is holding the shift key
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? superSonicSpeed : speed;
        moveDirection *= currentSpeed;

        // Apply gravity
        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalLookRotation += mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        // Move the player
        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime);
    }
}
