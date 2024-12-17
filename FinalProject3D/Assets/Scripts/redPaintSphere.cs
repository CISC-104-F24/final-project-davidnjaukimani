using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Plane"))
        {
            // Get the Renderer component of the plane
            Renderer planeRenderer = collision.gameObject.GetComponent<Renderer>();

            if (planeRenderer != null)
            {
                
                planeRenderer.material.color = Color.red;
            }
        }
    }
}

