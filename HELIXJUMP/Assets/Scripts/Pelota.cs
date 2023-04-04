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
    public float velocidadMinimaParaRomper = 0.1f;
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
        Debug.Log(Mathf.Abs(pelotaRigidbody.velocity.y) >= velocidadMinimaParaRomper);  
        // trail.SetActive(pelotaRigidbody.velocity.y < -10.0f);
     }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma")  && Mathf.Abs(pelotaRigidbody.velocity.y) <= velocidadMinimaParaRomper)
        {  
            
            collision.gameObject.SetActive(false);
            
        }
        else if (collision.gameObject.CompareTag("Plataformamala"))
        {
            if ( Mathf.Abs(pelotaRigidbody.velocity.y) <= velocidadMinimaParaRomper)

            {
                collision.gameObject.SetActive(false);
            }
            else
            {
                SistemadePuntos.instance.resetPuntos();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
           
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
