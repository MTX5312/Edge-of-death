using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFall : MonoBehaviour
{
    public float limitY = -5f;

    void Update()
    {
        if (transform.position.y < limitY)
        {
            Destroy(gameObject);
        }
    }
}