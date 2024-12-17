using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // Movement speed  
    public float speed = 5.0f;

    // Update is called once per frame  
    void Update()
    {
        // Move the cube based on user input  
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}