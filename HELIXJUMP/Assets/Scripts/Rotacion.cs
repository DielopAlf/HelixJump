using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public float velocidadRotacion = 10f;
    public int direccionRotacion = 0;
   // [SerializeField] float speed = 5f;
    
    void Update()
    {
        float direccion = Input.GetAxis("Horizontal");

        if(direccion > 0) {
            direccionRotacion = 1;
        } else if(direccion < 0) {
            direccionRotacion = -1;
        } else {
            direccionRotacion = 0;
           // Vector3 rot = new Vector3(0f,Input.GetAxis("Horizontal")*speed * Time.deltaTime,0f);
            //transform.Rotate(rot);
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, direccionRotacion * velocidadRotacion * Time.fixedDeltaTime);
    }
}