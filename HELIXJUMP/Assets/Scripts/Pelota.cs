using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pelota : MonoBehaviour
{
    public Rigidbody pelotaRigidbody;
    public float jumpForce = 2f;
    public float ultraSpeed = 3f;
    [SerializeField] GameObject trail;
    public float velocidadMinimaParaRomper = 50f;
    public float tiempoDeInvencibilidad = 2f;
    private bool invencible = false;
    public float velocidadNormal;

    private void Awake()
    {
        pelotaRigidbody = GetComponent<Rigidbody>();
        trail.SetActive(false);
        velocidadNormal = ultraSpeed;
    }

    void Update()
    {
        trail.SetActive(pelotaRigidbody.velocity.y < -ultraSpeed);
    }

  private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Plataforma"))
    {
        if (Mathf.Abs(pelotaRigidbody.velocity.y) <= velocidadMinimaParaRomper)
        {
            // Rebota en la plataforma
            pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    else if (collision.gameObject.CompareTag("Plataformamala"))
    {
        // Reinicia el juego si choca con una plataforma mala
        SistemadePuntos.instance.resetPuntos();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    else if (collision.gameObject.CompareTag("META"))
    {
        SistemadePuntos.instance.gameOver();
        gameObject.SetActive(false);
        InterfazController.instance.MostrarPanelVictoria();
    }
}

    IEnumerator TiempoInvencible()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempoDeInvencibilidad);
        invencible = false;
    }

    public void ActivarPowerUp()
    {
        ultraSpeed *= 2;
        StartCoroutine(DesactivarPowerUp());
    }

    IEnumerator DesactivarPowerUp()
    {
        yield return new WaitForSeconds(5f);
        ultraSpeed = velocidadNormal;
    }
}
