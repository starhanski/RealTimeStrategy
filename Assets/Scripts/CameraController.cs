using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed  = 100f, speed = 10f, zoomSpeed = 1000f;
     private bool isRotating = false;
    private Vector3 lastMousePosition;
    private float _mult = 1f;
    private void Update() {
        HandleInput();
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");



        float rotate = 0f;
        
        if(Input.GetKey(KeyCode.Q))
        rotate = -1f;
        else if(Input.GetKey(KeyCode.E))
        rotate = 1f;

        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;
        transform.Rotate(Vector3.up * rotationSpeed  * Time.deltaTime * rotate * _mult, Space.World);
        transform.Translate(new Vector3(hor, 0 ,ver) * Time.deltaTime* _mult * speed, Space.Self);
        transform.position += transform.up * zoomSpeed * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel");
        transform.position = new Vector3(
            Math.Clamp(transform.position.x,-100f,100f),
            Mathf.Clamp(transform.position.y,-26f, -10f) ,
            Mathf.Clamp(transform.position.z,-100f, 100f)
        );
    }
    void HandleInput()
    {
        if (Input.GetMouseButtonDown(2)) // Проверяем, была ли нажата средняя кнопка мыши (колесо)
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2)) // Проверяем, была ли отпущена средняя кнопка мыши (колесо)
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationY = -deltaMouse.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);

            lastMousePosition = Input.mousePosition;
            
        }
    }
}
