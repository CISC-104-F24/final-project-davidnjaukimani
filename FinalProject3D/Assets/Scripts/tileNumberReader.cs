
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneNamePrinter : MonoBehaviour
{
    // Adjust this offset to ensure the ray starts from above the character
    public float raycastOffset = 0.5f;
    // Maximum raycast distance
    public float raycastDistance = 10f;

    // Track whether the cube is currently touching a plane
    private bool isTouchingPlane = false;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the cube is touching a plane
        if (collision.gameObject.CompareTag("Plane"))
        {
            isTouchingPlane = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Remove plane touching status when leaving plane
        if (collision.gameObject.CompareTag("Plane"))
        {
            isTouchingPlane = false;
        }
    }

    void Update()
    {
        // Start the raycast slightly above the character's position
        Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;

        // Cast a ray downward
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, raycastDistance))
        {
            // Check if the ray hit something with a collider
            if (hitInfo.collider != null)
            {
                // Check if the hit object has the "Player" tag
                if (hitInfo.collider.CompareTag("Player"))
                {
                    // Ignore the hit object if it has the Player tag
                    return;
                }

                // Print the name of the object hit by the ray (if it doesn't have the "Player" tag)
                Debug.Log("Standing on: " + hitInfo.collider.gameObject.name);

                // Restart scene ONLY if standing on a mine AND touching a plane
                if (hitInfo.collider.gameObject.name == "mine" && isTouchingPlane)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    return;
                }

                // Paint tile green if not already painted red or green
                if (!IsTileAlreadyPainted(hitInfo.collider.gameObject, Color.red) &&
                    !IsTileAlreadyPainted(hitInfo.collider.gameObject, Color.green))
                {
                    HighlightObject(hitInfo.collider.gameObject, Color.green);
                }

                // Check if E key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // If the tile is not already red, paint it red
                    if (!IsTileAlreadyPainted(hitInfo.collider.gameObject, Color.red))
                    {
                        HighlightObject(hitInfo.collider.gameObject, Color.red);
                    }
                }
            }
        }
    }

    void HighlightObject(GameObject obj, Color color)
    {
        // Check if the object has a Renderer component
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            MaterialPropertyBlock props = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(props);
            // Set the color
            props.SetColor("_Color", color);
            renderer.SetPropertyBlock(props);
        }
    }

    bool IsTileAlreadyPainted(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            MaterialPropertyBlock props = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(props);
            // Compare the current color with the specified color
            return props.GetColor("_Color") == color;
        }
        return false;
    }
}