using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptJugador : MonoBehaviour
{
    private float speed = 15f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

//        if (speed > 30)
//        {
//            speed + 1;
//        }

        transform.Translate(new Vector3(x, 0, y) * Time.deltaTime * speed);
    }
}