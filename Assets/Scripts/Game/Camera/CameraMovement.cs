using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float defaultSpeed;
    public float boostSpeed;
    public float sensitivity;
    public float rotateSpeed;
    public float rotateSensitivity;

    void Update()
    {
        MovementCamera();
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseInputs = new Vector3
                (
                    -Input.GetAxis("Mouse Y"),
                    Input.GetAxis("Mouse X"),
                    0
                );

            transform.Rotate(mouseInputs * rotateSensitivity * rotateSpeed * Time.deltaTime);

            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        }
    }

    private void MovementCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 KeyboardInputs = new Vector3(horizontal, 0f, vertical);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            defaultSpeed *= boostSpeed;
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(boostSpeed != 0f)
            {
                defaultSpeed /= boostSpeed;
            }
        }

        transform.Translate(KeyboardInputs * defaultSpeed * Time.deltaTime);

    }
}
