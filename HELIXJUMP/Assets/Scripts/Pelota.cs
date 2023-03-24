using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    [SerializeField]
    Rigidbody PELOTArigidbody;

    [SerializeField]
    public float jumpForce = 2f;

    [SerializeField]
    GameObject trail;

    void awake()
    {

        PELOTArigidbody.GetComponent<Rigidbody>();
        trail.SetActive(false);

    }



    void Update()
    {
        //  Debug.Log(PELOTArigidbody.velocity.y);
        trail.SetActive(PELOTArigidbody.velocity.y < -10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hachocado");
        PELOTArigidbody.velocity = Vector3.zero;
        PELOTArigidbody.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);

    }




}
