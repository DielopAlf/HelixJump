using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    Rigidbody PELOTArigidbody;

    void awake()
    {

        PELOTArigidbody.GetComponent<Rigidbody>();

    }



    void Update()
    {
      //  Debug.Log(PELOTArigidbody.velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hachocado");
    }




}
