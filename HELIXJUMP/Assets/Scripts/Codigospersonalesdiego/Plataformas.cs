using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformas : MonoBehaviour
{
    private Transform player;
    public GameObject[] plataformasMalas;
    public float fuerzaExplosion = 500f;
    public float radioExplosion = 100f;
    public float velocidadMinimaParaDestruir = 0.1f;
    public float velocidadParaRomperse = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pelota"))
        {
            Pelota pelota = collision.gameObject.GetComponent<Pelota>();
            if (pelota.pelotaRigidbody.velocity.y <= 0)
            {
                if (pelota.ultraSpeed > pelota.velocidadNormal)
                {
                    SistemadePuntos.instance.resetPuntos();
                    DestruirPlataforma();
                }
                else if (pelota.pelotaRigidbody.velocity.magnitude >= velocidadParaRomperse)
                {
                    DestruirPlataforma();
                }
            }
        }
    }

    private void DestruirPlataforma()
    {
        for (int i = 0; i < plataformasMalas.Length; i++)
        {
            Rigidbody rb = plataformasMalas[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                Collider[] colliders = Physics.OverlapSphere(plataformasMalas[i].transform.position, radioExplosion);

                foreach (Collider newCollider in colliders)
                {
                    Rigidbody otherRB = newCollider.GetComponent<Rigidbody>();
                    if (otherRB != null && otherRB.velocity.magnitude >= velocidadMinimaParaDestruir)
                    {
                        otherRB.AddExplosionForce(fuerzaExplosion, plataformasMalas[i].transform.position, radioExplosion);
                    }
                }
            }

            if (plataformasMalas[i].CompareTag("Plataformamala"))
            {
                SistemadePuntos.instance.resetPuntos();
            }

            plataformasMalas[i].transform.parent = null;
            Destroy(plataformasMalas[i].gameObject, 50f);
        }

        Destroy(gameObject, 50f);
        this.enabled = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    private void Update()
    {
        if (player.gameObject != null && transform.position.y > player.position.y)
        {
            if (plataformasMalas.Length == 0)
            {
                DestruirPlataforma();
            }
        }
    }
}
