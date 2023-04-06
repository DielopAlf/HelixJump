using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    private Transform player;
    public GameObject[] plataformasmalas;
    float force = 500f;
    float radius = 100f;
    public float velocidadMinimaParaDestruir = 50f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pelota"))
        {
            Pelota pelota = collision.gameObject.GetComponent<Pelota>();
            if (pelota.pelotaRigidbody.velocity.y <= 0)
            {
                if (pelota.ultraSpeed > pelota.velocidadNormal)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    private void Update()
    {
        if (player.gameObject != null && transform.position.y > player.position.y)
        {
            for (int i = 0; i < plataformasmalas.Length; i++)
            {
                Rigidbody rb = plataformasmalas[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;

                    Collider[] colliders = Physics.OverlapSphere(plataformasmalas[i].transform.position, radius);

                    foreach (Collider newCollider in colliders)
                    {
                        Rigidbody otherRB = newCollider.GetComponent<Rigidbody>();
                        if (otherRB != null && otherRB.velocity.magnitude >= velocidadMinimaParaDestruir)
                        {
                            otherRB.AddExplosionForce(force, plataformasmalas[i].transform.position, radius);
                        }
                    }
                }

                plataformasmalas[i].transform.parent = null;
                Destroy(plataformasmalas[i].gameObject, 50f);
            }

            Destroy(gameObject, 50f);
            this.enabled = false;
        }
    }
}
