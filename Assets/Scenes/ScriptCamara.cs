using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamara : MonoBehaviour
{
    public float MouseX;
    public float MouseY;

    public Transform Body;
    public Transforma Head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X") * 100 * Time.deltaTime;
        Body.Rotate(Vector3.up, MouseX);

        MouseY = Input.GetAxis("MouseY") * 100 * Time.deltatime;
        Head.Rotate(Vector3.left, MouseY);
    }
}
