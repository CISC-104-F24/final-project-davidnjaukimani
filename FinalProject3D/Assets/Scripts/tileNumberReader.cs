using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneNamePrinter : MonoBehaviour
{
    
    public float raycastOffset = 0.5f;

    
    public float raycastDistance = 10f;

    void Update()
    {
        
        Vector3 rayOrigin = transform.position + Vector3.up * raycastOffset;

        // Cast a ray downward
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, raycastDistance))
        {
           
            if (hitInfo.collider != null)
            {
               
                if (hitInfo.collider.CompareTag("Player"))
                {
                   
                    return;
                }

                
                Debug.Log("Standing on: " + hitInfo.collider.gameObject.name);
            }
        }
    }
}

