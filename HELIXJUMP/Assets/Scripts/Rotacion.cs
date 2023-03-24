using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public float velocidadRotacion = 10f;
    public int direccionRotacion = 0;

    // Update is called once per frame
    void Update()
    {
        float direccion = Input.GetAxis("Horizontal");

        if(direccion > 0) {
            direccionRotacion = 1;
        } else if(direccion < 0) {
            direccionRotacion = -1;
        } else {
            direccionRotacion = 0;
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, direccionRotacion * velocidadRotacion * Time.fixedDeltaTime);
    }
}