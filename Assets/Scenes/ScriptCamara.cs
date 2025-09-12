using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public float MouseX;
    public Transform Body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        Body.Rotate(Vector3.up, MouseX);
    }
}
