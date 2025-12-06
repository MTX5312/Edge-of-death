using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOroGrande : MonoBehaviour
{
    [Header("Movimiento")]
    public float fallSpeed = 2f;
    public float resetHeight;
    public float resetDelay = 1f;

    [Header("Rotación")]
    public float rotationSpeed = 1f;

    private Vector3 originalPosition;
    private bool isResetting = false;

    private void Start()
    {
        originalPosition = transform.position;
        resetHeight = originalPosition.y - 20f;
    }

    private void Update()
    {
        if (!isResetting)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        if (transform.position.y <= resetHeight && !isResetting)
        {
            StartCoroutine(ResetPosition());
        }
    }

    private IEnumerator ResetPosition()
    {
        isResetting = true;

        yield return new WaitForSeconds(resetDelay);

        transform.position = originalPosition;

        isResetting = false;
    }
}