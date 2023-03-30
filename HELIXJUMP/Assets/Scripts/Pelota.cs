using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pelota : MonoBehaviour
{
    public Rigidbody pelotaRigidbody;

    public float jumpForce = 2f;
  
    [SerializeField] float ultraSpeed = 3f;
    [SerializeField] GameObject trail;
    public float velocidadMinimaParaRomper = 0.0001f;
    public float tiempoDeInvencibilidad = 2f;
    //private bool invencible = false;
    

    private void Awake()
    {
        pelotaRigidbody = GetComponent<Rigidbody>();
         trail.SetActive(false);
    }

     void Update()
     {
         trail.SetActive(pelotaRigidbody.velocity.y < -ultraSpeed);
        // trail.SetActive(pelotaRigidbody.velocity.y < -10.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma") && pelotaRigidbody.velocity.y <= -velocidadMinimaParaRomper)
        {
           // if (!invencible)
            {
               collision.gameObject.SetActive(false);
               // DestroyImmediate(collision.gameObject);
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
          //  StartCoroutine(TiempoInvencible());
        }

        /* if (pelotaRigidbody.velocity.y < -ultraSpeed)
         {
          Destroy(collision.gameObject);

         }
         else
         {
             pelotaRigidbody.velocity=Vector3.zero;
             pelotaRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
         }*/



    }

   /* IEnumerator TiempoInvencible()
    {
        invencible = true;
        yield return new WaitForSeconds(tiempoDeInvencibilidad);
        invencible = false;
    }
    */





}
