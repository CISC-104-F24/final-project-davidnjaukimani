using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    //variables
    public float sensitivityY = 0.5f;
    public Vector3 mouseMovement;

    void Start()
    {
    }

    void Update()
    {
        mouseMovement.y += Input.GetAxis("Mouse Y") * sensitivityY;
        transform.localEulerAngles = new Vector3(-mouseMovement.y, 0, 0);

    }
}
