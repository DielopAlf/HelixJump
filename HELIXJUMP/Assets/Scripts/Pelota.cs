using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    [SerializeField]
    Rigidbody pelotaRigidbody;

    [SerializeField]
    public float jumpForce = 2f;

    [SerializeField]
    GameObject trail;

    [SerializeField]
    float velocidadMinimaParaRomper = 10f;

    void Awake()
    {
        pelotaRigidbody = GetComponent<Rigidbody>();
        trail.SetActive(false);
    }

    void Update()
    {
        trail.SetActive(pelotaRigidbody.velocity.y < -10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plataforma") && pelotaRigidbody.velocity.magnitude >= velocidadMinimaParaRomper) {
            Destroy(collision.gameObject);
        } else {
            pelotaRigidbody.velocity = Vector3.zero;
            pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
