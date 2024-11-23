using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float boostSpeed = 5.0f;

    private Rigidbody playerRigidbody;

    private bool isSprinting;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    void Update()
    {
      
    }

    void FixedUpdate()
    {
        // MOVE WITH W,A, S and D
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

        // Get camera orientation
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Ensure movement is only on the XZ plane
        forward.y = 0f;
        right.y = 0f;

        // Normalize directional vectors
        forward = forward.normalized;
        right = right.normalized;

        // Calculate movement direction relative to the camera
        Vector3 movement = (forward * verticalInput + right * horizontalInput).normalized;

        // Determine movement speed
        float currentSpeed = isSprinting ? boostSpeed : moveSpeed;

        // Apply movement
        playerRigidbody.MovePosition(transform.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
