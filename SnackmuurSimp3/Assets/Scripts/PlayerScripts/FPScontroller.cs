using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    public Transform cam;
    public float speed = 5f;
    public float mouseSensitivity = 200f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 1.1f;

    CharacterController controller;
    float xRotation = 0f;
    float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- MOUSE LOOK ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        if (cam) cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // --- INPUT ---
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // Desired move in local space
        Vector3 desiredMove = transform.right * x + transform.forward * z;
        desiredMove = desiredMove.normalized;

        // --- GROUND NORMAL (project m ovement onto the slope) ---
        RaycastHit hit;
        Vector3 groundNormal = Vector3.up;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckDistance))
        {
            groundNormal = hit.normal;
        }

        // Project desired move onto plane defined by ground normal so we move along the slope
        Vector3 moveAlongSlope = Vector3.ProjectOnPlane(desiredMove, groundNormal).normalized;

        // --- GRAVITY / STICKING ---
        if (controller.isGrounded)
        {
            // small negative to keep contact
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = (moveAlongSlope * speed) + new Vector3(0f, verticalVelocity, 0f);

        // Move using CharacterController (this respects collisions + slopes)
        controller.Move(finalMove * Time.deltaTime);
    }
}
