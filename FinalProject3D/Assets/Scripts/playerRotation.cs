using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotation : MonoBehaviour
{
    //variables
    public float sensitivityX = 2.0f;
    public Vector3 mouseMovement;

  
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    { 
        mouseMovement.x += Input.GetAxis("Mouse X") * sensitivityX;
        transform.localEulerAngles = new Vector3 (0, mouseMovement.x, 0);
        
    }
}
