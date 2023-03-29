using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pelota : MonoBehaviour
{
    public Rigidbody pelotaRigidbody;
    public float jumpForce = 2f;
    public GameObject trail;
    public float velocidadMinimaParaRomper = 10f;
    public float tiempoDeInvencibilidad = 2f;
    private bool invencible = false;

    private void Awake()
    {
        pelotaRigidbody = GetComponent<Rigidbody>();
        // trail.SetActive(false);
    }

    private void Update()
    {
        // trail.SetActive(pelotaRigidbody.velocity.y < -10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma") && pelotaRigidbody.velocity.magnitude >= velocidadMinimaParaRomper)
        {
            if (!invencible)
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Plataformamala"))
        {
            SistemadePuntos.instance.resetPuntos();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (collision.gameObject.CompareTag("META"))
        {
            SistemadePuntos.instance.gameOver();
            gameObject.SetActive(false);
            InterfazController.instance.MostrarPanelVictoria();
        }
        else
        {
            pelotaRigidbody.velocity = Vector3.zero;
            pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(TiempoInvencible());
        }
    }

    IEnumerator TiempoInvencible()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempoDeInvencibilidad);
        invencible = false;
    }
}
