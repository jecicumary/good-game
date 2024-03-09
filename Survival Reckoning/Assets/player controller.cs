using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float runningSpeedMultiplier = 2f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    private CharacterController characterController;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Player movement
        float moveForward = Input.GetAxis("Vertical") * movementSpeed;
        float moveSideways = Input.GetAxis("Horizontal") * movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveForward *= runningSpeedMultiplier;
            moveSideways *= runningSpeedMultiplier;
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpForce;
        }

        Vector3 movement = new Vector3(moveSideways, verticalVelocity, moveForward);
        characterController.Move(transform.TransformDirection(movement) * Time.deltaTime);

        // Player rotation
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, horizontalRotation, 0f);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}