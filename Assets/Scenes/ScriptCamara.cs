using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public float MouseX;
    public float MouseY;
    public float sensibilidad = 150f;

    public Transform Body;
    public Transform Head;

    public bool camaraMovida;

    public float Angle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        camaraMovida = false;

        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        if (Mathf.Abs(mouseX) > 0.001f) // Detecta si realmente hubo movimiento
        {
            Body.Rotate(Vector3.up, mouseX);
            camaraMovida = true;
        }

        // Movimiento vertical
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
        if (Mathf.Abs(mouseY) > 0.001f)
        {
            Angle -= mouseY;
            Angle = Mathf.Clamp(Angle, -30, 45);
            Head.localRotation = Quaternion.Euler(Angle, 0, 0);
            camaraMovida = true;
        }
    }
}
