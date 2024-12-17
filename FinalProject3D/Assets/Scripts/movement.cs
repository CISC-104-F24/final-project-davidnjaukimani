using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // Movement speed  
    public float normalSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;
    public float jumpForce = 50f;
    public float additionalGravity = 0.5f;

    private float currentSpeed;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // If no Rigidbody is attached, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Adjust physics properties for better jump feel
        rb.drag = 0.5f;
        rb.useGravity = true;
    }

    void Update()
    {
        // Check if Shift key is held down for sprinting
        currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)
            ? normalSpeed * sprintMultiplier
            : normalSpeed;

        // Horizontal Movement
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.D))
        {
            movement += Vector3.forward * currentSpeed;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A))
        {
            movement += Vector3.back * currentSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.W))
        {
            movement += Vector3.left * currentSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S))
        {
            movement += Vector3.right * currentSpeed;
        }

        // Apply horizontal movement
        transform.Translate(movement * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Add additional gravity for more controlled jump
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Acceleration);
        }
    }

    // Check ground contact
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a plane
        if (collision.gameObject.CompareTag("Plane"))
        {
            isGrounded = true;
        }
    }
}