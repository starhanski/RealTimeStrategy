using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlaceObjects : MonoBehaviour
{

    public LayerMask layer;
    public float rotateSpeed = 6000f;
    private bool isRotating = false;
    private Vector3 fixedPosition;
    public LayerMask obstacleLayer;
    public bool isPlacementValid = true;
    public int HangarCost = 100;
    private MoneyManager moneyManager;

    private void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
        PositionObjects();

    }

    private void Update()
    {
        if (isRotating == false)
        {
            PositionObjects();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RotateBuilding();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (moneyManager.CanAfford(HangarCost))
            {
                gameObject.GetComponent<AutoCarCreate>().enabled = true;
                Destroy(gameObject.GetComponent<PlaceObjects>());
                moneyManager.SpendMoney(HangarCost);
            }

        }

    }

    public void PositionObjects()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, layer))
        {

            transform.position = hit.point;
        }


    }


    void RotateBuilding()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем, была ли нажата средняя кнопка мыши (колесо)
        {
            isRotating = true;
            fixedPosition = transform.position;
        }

        if (Input.GetMouseButtonUp(0)) // Проверяем, была ли отпущена средняя кнопка мыши (колесо)
        {
            isRotating = false;
            PositionObjects();
        }

        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float rotationY = rotateSpeed * Time.deltaTime * mouseX;
            transform.position = fixedPosition;
            transform.Rotate(Vector3.up, rotationY);

        }
    }
}
